using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TourPlanner.Logic.Service;
using TourPlanner.Model;
using TourPlanner.Views;

namespace TourPlanner.ViewModels {

    internal class MainViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _filterText = "";
        public string FilterText {
            get => _filterText;
            set {
                _filterText = value;
                PropertyChanged?.Invoke(this, new(nameof(FilterText)));
                FilterTours();
            }
        }

        public ObservableCollection<Tour> Tours { get; private set; } = new();

        private Tour? _selectedTour;
        public Tour? SelectedTour {
            get => _selectedTour;
            set {
                _selectedTour = value;
                PropertyChanged?.Invoke(this, new(nameof(SelectedTour)));
            }
        }

        private ImageSource _mapImage = new BitmapImage();
        public ImageSource MapImage {
            get => _mapImage;
            set {
                _mapImage = value;
                PropertyChanged?.Invoke(this, new(nameof(MapImage)));
            }
        }


        public ObservableCollection<TourLog> TourLogs { get; private set; } = new();

        public RelayCommand AddTourCommand { get; private set; }
        public RelayCommand EditTourCommand { get; private set; }
        public RelayCommand DeleteTourCommand { get; private set; }

        private readonly ITourService _tourService;
        private readonly ITourLogService _tourLogService;
        private readonly IRouteService _routeService;
        private readonly IMapService _mapService;
        private readonly TourDialogViewModel _tourDialogViewModel;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public MainViewModel(ITourService tourService, ITourLogService tourLogService, IRouteService routeService, IMapService mapService, TourDialogViewModel tourDialogViewModel) {
            _tourService = tourService;
            _tourLogService = tourLogService;
            _routeService = routeService;
            _mapService = mapService;
            _tourDialogViewModel = tourDialogViewModel;
            AddTourCommand = new(x => AddTour());
            EditTourCommand = new RelayCommand(x => EditTour());
            DeleteTourCommand = new RelayCommand(x => DeleteTour());
            FetchTours();
        }

        private void FetchTours() {
            Tours.Clear();
            TourLogs.Clear();
            _tourService.GetAll().ForEach(tour => Tours.Add(tour));

            if (Tours.Any()) {
                SelectedTour = Tours[0];
                FetchSelectedTour();
            }
        }

        private void FetchSelectedTour() {
            _tourLogService.GetByTour(SelectedTour!).ForEach(tourLog => TourLogs.Add(tourLog));

            if (SelectedTour!.RouteFetched) {
                using var ms = new MemoryStream(SelectedTour.MapImage!);
                var bitmap = new Bitmap(ms);
                MapImage = BitmapToImageSource(bitmap);
            } else if (SelectedTour.From != null && SelectedTour.To != null) {
                FetchRouteData();
            }
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void FilterTours() {
            Tours.Clear();
            _tourService.GetByNameContains(FilterText).ForEach(tour => Tours.Add(tour));
        }

        public void SelectTour(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Tour tour) {
                SelectedTour = tour;
                FetchSelectedTour();
            }
        }

        public void AddTour() {
            var tourDialog = new TourDialog(_tourDialogViewModel);
            if (tourDialog.ShowDialog() == true) {
                FetchTours();
            }
        }

        public void EditTour() {
            var tourDialog = new TourDialog(_tourDialogViewModel, new(SelectedTour!));
            if (tourDialog.ShowDialog() == true) {
                FetchTours();
            }
        }

        public void DeleteTour() {
            _tourService.Remove(SelectedTour!);
            FetchTours();
        }

        /// <summary>
        ///     Fetches tour data and map image from route service and map service
        ///     and updates the selected tour and map image.
        ///     Requires the tour to have a from and to location.
        /// </summary>
        private void FetchRouteData() {
            try {
                // Fetch route
                _routeService.GetRoute(SelectedTour!.From!, SelectedTour.To!, SelectedTour.TransportType).ContinueWith(task => {
                    var route = task.Result;

                    SelectedTour.RouteFetched = true;
                    SelectedTour.LastFetched = DateTime.UtcNow;
                    SelectedTour.Distance = route.Distance;
                    SelectedTour.EstimatedTime = route.Time;

                    // Fetch map of route
                    _mapService.GetMap(route).ContinueWith(task => {
                        var bitmap = task.Result;

                        using var stream = new MemoryStream();
                        bitmap.Save(stream, ImageFormat.Png);

                        SelectedTour.MapImage = stream.ToArray();

                        // Update tour and map image in UI thread
                        Application.Current.Dispatcher.Invoke(() => {
                            int index = Tours.IndexOf(SelectedTour);
                            SelectedTour = _tourService.Update(SelectedTour);
                            Tours[index] = SelectedTour;
                            MapImage = BitmapToImageSource(bitmap);
                        });
                    });
                });
            } catch (Exception e) {
                // TODO: Show error
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
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
    }
}
