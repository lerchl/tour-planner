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

        private double? _childFriendliness;
        public double? ChildFriendliness {
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

            InUI(() => {
                RaisePropertyChanged(nameof(Distance));
                RaisePropertyChanged(nameof(EstimatedTime));
            });

            if (Tour == null) {
                ClearPopularityRank();
                ClearChildFriendliness();
                ClearMap();
                return;
            }

            ClearPopularityRank(true);
            ClearChildFriendliness(true);
            ClearMap(true);

            int popularityRank = _tourService.GetPopularityRank(Tour);
            ShowPopularityRank(popularityRank);

            if (Tour.LastFetched != null) {
                // Route has been fetched, map image should therefore be available
                using var stream = new MemoryStream(Tour.MapImage!);
                var bitmap = new Bitmap(stream);
                ShowMap(bitmap);
            } else if (Tour.From != null && Tour.To != null) {
                // From and to are set, can therefore fetch route
                await FetchRoute();
            }

            double childFriendliness = _tourService.GetChildFriendliness(Tour);
            ShowChildFriendliness(childFriendliness);
        }

        public void UpdatePopularityRankAndChildFriendliness() {
            ClearPopularityRank(true);
            ClearChildFriendliness(true);
            int popularityRank = _tourService.GetPopularityRank(Tour!);
            double childFriendliness = _tourService.GetChildFriendliness(Tour!);
            ShowPopularityRank(popularityRank);
            ShowChildFriendliness(childFriendliness);
        }

        private void ClearPopularityRank(bool showLoading = false) {
            InUI(() => {
                PopularityRank = null;

                if (showLoading) {
                    PopularityRankLoadingViewModel?.Show();
                }
            });
        }

        private void ClearChildFriendliness(bool showLoading = false) {
            InUI(() => {
                ChildFriendliness = null;

                if (showLoading) {
                    ChildFriendlinessLoadingViewModel?.Show();
                }
            });
        }

        private void ShowPopularityRank(int popularityRank) {
            InUI(() => {
                PopularityRankLoadingViewModel?.Hide();
                PopularityRank = popularityRank;
            });
        }

        private void ShowChildFriendliness(double childFriendliness) {
            InUI(() => {
                ChildFriendlinessLoadingViewModel?.Hide();
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
                Tour.LastFetched = DateTime.Now;
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
