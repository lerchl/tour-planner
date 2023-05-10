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

            // TODO: fetch route should be disabled if tour is null
            // or from or to are not set
            // TODO: manual fetch does not hide map or show loading indicator
            FetchRouteCommand = new(async x => await FetchRoute());
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public async void LoadTourDetails(Tour tour) {
            Tour = tour;
            InUI(() => {
                ClearTourDetails();
                RaisePropertyChanged(nameof(Distance));
                RaisePropertyChanged(nameof(EstimatedTime));
            });

            int popularityRank = _tourService.GetPopularityRank(tour);
            // TODO: Implement child friendliness
            int childFriendliness = 0;

            InUI(() => {
                PopularityRankLoadingViewModel?.Hide();
                ChildFriendlinessLoadingViewModel?.Hide();
                PopularityRank = popularityRank;
                ChildFriendliness = childFriendliness;
            });

            if (Tour.RouteFetched) {
                // Route has been fetched, map image should therefore be available
                using var stream = new MemoryStream(Tour.MapImage!);
                var bitmap = new Bitmap(stream);

                InUI(() => {
                    MapLoadingViewModel?.Hide();
                    MapImage = BitmapToImageSource(bitmap);
                });
            } else if (Tour.From != null && Tour.To != null) {
                // From and to are set, can therefore fetch route
                await FetchRoute();
            }
        }

        private void ClearTourDetails() {
            PopularityRank = null;
            ChildFriendliness = null;
            MapImage = null;
            PopularityRankLoadingViewModel?.Show();
            ChildFriendlinessLoadingViewModel?.Show();
            MapLoadingViewModel?.Show();
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
                // TODO: comment why this line is okay :)
                var route = await _routeService.GetRoute(Tour!.From!, Tour.To!, Tour.TransportType);
                ShowDistanceAndTime(route);

                var bitmap = await _mapService.GetMap(route);
                using var stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);
                ShowMapImage(bitmap);

                Tour.RouteFetched = true;
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

        private void ShowMapImage(Bitmap bitmap) {
            InUI(() => {
                MapLoadingViewModel?.Hide();
                MapImage = BitmapToImageSource(bitmap);
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

        private void ClearDistanceAndTime() {
            InUI(() => {
                Distance = null;
                EstimatedTime = null;
                DistanceLoadingViewModel?.Show();
                EstimatedTimeLoadingViewModel?.Show();
            });
        }
    }
}
