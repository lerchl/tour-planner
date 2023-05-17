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
                InUI(FetchRouteCommand.RaiseCanExecuteChanged);
            }
        }

        public EventHandler? RouteFetched;

        public double? Distance {
            get => Tour?.Distance;
            set {
                Tour!.Distance = value;
                RaisePropertyChanged();
            }
        }

        public long? EstimatedTime {
            get => Tour?.EstimatedTime;
            set {
                Tour!.EstimatedTime = value;
                RaisePropertyChanged();
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

        private readonly ITourService _tourService;
        private readonly IRouteService _routeService;
        private readonly IMapService _mapService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourDetailsViewModel(ITourService _tourService, IRouteService _routeService, IMapService _mapService) {
            this._tourService = _tourService;
            this._routeService = _routeService;
            this._mapService = _mapService;

            FetchRouteCommand = new(
                async x => await FetchRoute(),
                x => Tour != null && Tour.From.Length > 0 && Tour.To.Length > 0
            );
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public async void LoadTourDetails(Tour tour) {
            Tour = tour;
            InUI(() => {
                RaisePropertyChanged(nameof(Distance));
                RaisePropertyChanged(nameof(EstimatedTime));
            });
            ClearPopularityRankAndChildFriendliness();
            ClearMap();

            int popularityRank = _tourService.GetPopularityRank(tour);
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

        private void ClearPopularityRankAndChildFriendliness() {
            InUI(() => {
                PopularityRank = null;
                ChildFriendliness = null;
                PopularityRankLoadingViewModel?.Show();
                ChildFriendlinessLoadingViewModel?.Show();
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

        private void ClearMap() {
            InUI(() => {
                MapImage = null;
                MapLoadingViewModel?.Show();
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
                ClearDistanceAndTime();
                ClearMap();
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
                // TODO: Show error
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private void ClearDistanceAndTime() {
            InUI(() => {
                Distance = null;
                EstimatedTime = null;
                DistanceLoadingViewModel?.Show();
                EstimatedTimeLoadingViewModel?.Show();
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
