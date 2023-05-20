using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourDetailsViewModel : BaseViewModel {

        private Tour? _tour;
        public Tour? Tour {
            get => _tour;
            set {
                _tour = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Distance));
                RaisePropertyChanged(nameof(EstimatedTime));
                InUI(FetchRouteCommand.RaiseCanExecuteChanged);
            }
        }

        public EventHandler? RouteFetched;

        public double? Distance {
            get => Tour?.Distance;
            set {
                if (Tour != null) {
                    Tour.Distance = value;
                    RaisePropertyChanged();
                }
            }
        }

        public long? EstimatedTime {
            get => Tour?.EstimatedTime;
            set {
                if (Tour != null) {
                    Tour.EstimatedTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ImageSource? _mapImage;
        public ImageSource? MapImage {
            get => _mapImage;
            set {
                _mapImage = value;
                RaisePropertyChanged();
            }
        }

        private int? _popularityRank;
        public int? PopularityRank {
            get => _popularityRank;
            set {
                _popularityRank = value;
                RaisePropertyChanged();
            }
        }

        private int? _childFriendliness;
        public int? ChildFriendliness {
            get => _childFriendliness;
            set {
                _childFriendliness = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand FetchRouteCommand { get; private set; }

        public LoadingIndicatorViewModel? PopularityRankLoadingViewModel { get; set; }
        public LoadingIndicatorViewModel? ChildFriendlinessLoadingViewModel { get; set; }
        public LoadingIndicatorViewModel? DistanceLoadingViewModel { get; set; }
        public LoadingIndicatorViewModel? EstimatedTimeLoadingViewModel { get; set; }
        public LoadingIndicatorViewModel? MapLoadingViewModel { get; set; }

        private readonly IShowErrorService _showErrorService;

        private readonly ITourService _tourService;
        private readonly IRouteService _routeService;
        private readonly IMapService _mapService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourDetailsViewModel(IShowErrorService showErrorService,
                                    ITourService tourService,
                                    IRouteService routeService,
                                    IMapService mapService) {
            _showErrorService = showErrorService;
            _tourService = tourService;
            _routeService = routeService;
            _mapService = mapService;

            FetchRouteCommand = new(
                async x => await FetchRoute(),
                x => Tour != null && Tour.From.Length > 0 && Tour.To.Length > 0
            );
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public async void LoadTourDetails(Tour? tour) {
            Tour = tour;

            if (Tour == null) {
                ClearPopularityRankAndChildFriendliness();
                ClearMap();
                return;
            }

            InUI(() => {
                RaisePropertyChanged(nameof(Distance));
                RaisePropertyChanged(nameof(EstimatedTime));
            });
            ClearPopularityRankAndChildFriendliness(true);
            ClearMap(true);

            int popularityRank = _tourService.GetPopularityRank(Tour);
            // TODO: Implement child friendliness
            int childFriendliness = 0;
            ShowPopularityRankAndChildFriendliness(popularityRank, childFriendliness);

            if (Tour.LastFetched != null) {
                // Route has been fetched, map image should therefore be available
                using var stream = new MemoryStream(Tour.MapImage!);
                var bitmap = new Bitmap(stream);
                ShowMap(bitmap);
            } else if (Tour.From != null && Tour.To != null) {
                // From and to are set, can therefore fetch route
                await FetchRoute();
            }
        }

        private void ClearPopularityRankAndChildFriendliness(bool showLoading = false) {
            InUI(() => {
                PopularityRank = null;
                ChildFriendliness = null;

                if (showLoading) {
                    PopularityRankLoadingViewModel?.Show();
                    ChildFriendlinessLoadingViewModel?.Show();
                }
            });
        }

        private void ShowPopularityRankAndChildFriendliness(int popularityRank, int childFriendliness) {
            InUI(() => {
                PopularityRankLoadingViewModel?.Hide();
                ChildFriendlinessLoadingViewModel?.Hide();
                PopularityRank = popularityRank;
                ChildFriendliness = childFriendliness;
            });
        }

        private void ClearMap(bool showLoading = false) {
            InUI(() => {
                MapImage = null;
                if (showLoading) {
                    MapLoadingViewModel?.Show();
                }
            });
        }

        private void ShowMap(Bitmap bitmap) {
            InUI(() => {
                MapLoadingViewModel?.Hide();
                MapImage = BitmapToImageSource(bitmap);
            });
        }

        private static ImageSource BitmapToImageSource(Bitmap bitmap) {
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try {
                var size = rect.Width * rect.Height * 4;
                return BitmapSource.Create(bitmap.Width,
                                           bitmap.Height,
                                           bitmap.HorizontalResolution,
                                           bitmap.VerticalResolution,
                                           PixelFormats.Bgra32,
                                           null,
                                           bitmapData.Scan0,
                                           size,
                                           bitmapData.Stride);
            } finally {
                bitmap.UnlockBits(bitmapData);
            }
        }

        private async Task FetchRoute() {
            try {
                ClearDistanceAndTime(true);
                ClearMap(true);
                // see FetchRouteCommand, will only be called if Tour is not null
                // and From and To are are not empty
                var route = await _routeService.GetRoute(Tour!.From!, Tour.To!, Tour.TransportType);
                ShowDistanceAndTime(route);

                var bitmap = await _mapService.GetMap(route);
                using var stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);
                ShowMap(bitmap);

                Tour.MapImage = stream.ToArray();
                Tour.LastFetched = DateTime.UtcNow;
                Tour = _tourService.Update(Tour);

                RouteFetched?.Invoke(this, EventArgs.Empty);
            } catch (Exception e) {
                _showErrorService.ShowError(e);
            }
        }

        private void ClearDistanceAndTime(bool showLoading = false) {
            InUI(() => {
                Distance = null;
                EstimatedTime = null;

                if (showLoading) {
                    DistanceLoadingViewModel?.Show();
                    EstimatedTimeLoadingViewModel?.Show();
                }
            });
        }

        private void ShowDistanceAndTime(Route route) {
            InUI(() => {
                Distance = route.Distance;
                EstimatedTime = route.Time;
                DistanceLoadingViewModel?.Hide();
                EstimatedTimeLoadingViewModel?.Hide();
            });
        }
    }
}
