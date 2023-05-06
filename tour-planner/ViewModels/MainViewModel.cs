using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

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

        private TourLog? _selectedTourLog;
        public TourLog? SelectedTourLog {
            get => _selectedTourLog;
            set {
                _selectedTourLog = value;
                PropertyChanged?.Invoke(this, new(nameof(SelectedTourLog)));
            }
        }

        public LoadingViewModel MapLoadingViewModel { get; set; }
        public LoadingViewModel DistanceLoadingViewModel { get; set; }
        public LoadingViewModel EstimatedTimeLoadingViewModel { get; set; }

        public ObservableCollection<TourLog> TourLogs { get; private set; } = new();

        public RelayCommand AddTourCommand { get; private set; }
        public RelayCommand FetchRouteDataCommand { get; private set; }
        public RelayCommand EditTourCommand { get; private set; }
        public RelayCommand DeleteTourCommand { get; private set; }

        public RelayCommand AddTourLogCommand { get; private set; }
        public RelayCommand EditTourLogCommand { get; private set; }

        private readonly ITourService _tourService;
        private readonly ITourLogService _tourLogService;
        private readonly IRouteService _routeService;
        private readonly IMapService _mapService;
        private readonly IDialogService _dialogService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public MainViewModel(ITourService tourService, ITourLogService tourLogService, IRouteService routeService, IMapService mapService, IDialogService dialogService) {
            _tourService = tourService;
            _tourLogService = tourLogService;
            _routeService = routeService;
            _mapService = mapService;
            _dialogService = dialogService;
            AddTourCommand = new(x => AddTour());
            FetchRouteDataCommand = new(x => FetchRouteData());
            EditTourCommand = new RelayCommand(x => EditTour());
            DeleteTourCommand = new RelayCommand(x => DeleteTour());
            AddTourLogCommand = new RelayCommand(x => AddTourLog());
            EditTourLogCommand = new RelayCommand(x => EditTourLog());
            FetchTours();
        }

        private void FetchTours() {
            Tours.Clear();
            _tourService.GetAll().ForEach(tour => Tours.Add(tour));

            if (Tours.Any()) {
                SelectedTour = Tours[0];
                FetchSelectedTour();
            }
        }

        private void FetchSelectedTour() {
            FetchTourLogs();

            if (SelectedTour!.RouteFetched) {
                using var ms = new MemoryStream(SelectedTour.MapImage!);
                var bitmap = new Bitmap(ms);
                MapImage = BitmapToImageSource(bitmap);
            } else if (SelectedTour.From != null && SelectedTour.To != null) {
                FetchRouteData();
            }
        }

        private void FetchTourLogs() {
            TourLogs.Clear();
            _tourLogService.GetByTour(SelectedTour!).ForEach(tourLog => TourLogs.Add(tourLog));
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void FilterTours() {
            Tours.Clear();
            _tourService.GetByNameContains(FilterText).ForEach(tour => Tours.Add(tour));
        }

        public void SelectTour(Tour tour) {
            SelectedTour = tour;
            FetchSelectedTour();
        }

        public void AddTour() {
            if (_dialogService.OpenAddTourDialog()) {
                FetchTours();
            }
        }

        public void EditTour() {
            if (_dialogService.OpenEditTourDialog(new(SelectedTour!))) {
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
            MapLoadingViewModel.Show();
            DistanceLoadingViewModel.Show();
            EstimatedTimeLoadingViewModel.Show();

            // Fetch route
            _routeService.GetRoute(SelectedTour!.From!, SelectedTour.To!, SelectedTour.TransportType).ContinueWith(task => {
                try {
                    var route = task.Result;

                    SelectedTour.RouteFetched = true;
                    SelectedTour.Distance = route.Distance;
                    SelectedTour.EstimatedTime = route.Time;

                    // Fetch map of route
                    _mapService.GetMap(route).ContinueWith(task => {
                        var bitmap = task.Result;

                        using var stream = new MemoryStream();
                        bitmap.Save(stream, ImageFormat.Png);

                        SelectedTour.MapImage = stream.ToArray();

                        MapLoadingViewModel.Hide();
                        DistanceLoadingViewModel.Hide();
                        EstimatedTimeLoadingViewModel.Hide();

                        Application.Current.Dispatcher.Invoke(() => {
                            int index = Tours.IndexOf(SelectedTour);

                            // setting last fetched as late as possible
                            SelectedTour.LastFetched = DateTime.UtcNow;

                            SelectedTour = _tourService.Update(SelectedTour);
                            Tours[index] = SelectedTour;
                            MapImage = BitmapToImageSource(bitmap);
                        });
                    });
                } catch (Exception e) {
                    // TODO: Show error
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
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

        public void SelectTourLog(TourLog tourLog) {
            SelectedTourLog = tourLog;
        }

        public void AddTourLog() {
            if (_dialogService.OpenAddTourLogDialog(SelectedTour!)) {
                FetchTourLogs();
            }
        }

        public void EditTourLog() {
            if (_dialogService.OpenEditTourLogDialog(SelectedTourLog!)) {
                FetchTourLogs();
            }
        }
    }
}
