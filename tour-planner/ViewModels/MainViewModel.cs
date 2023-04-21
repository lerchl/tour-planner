using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
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

        private Tour _selectedTour = new();
        public Tour SelectedTour {
            get => _selectedTour;
            set {
                _selectedTour = value;
                PropertyChanged?.Invoke(this, new(nameof(SelectedTour)));
            }
        }

        public ObservableCollection<TourLog> TourLogs { get; private set; } = new();

        public RelayCommand AddTourCommand { get; private set; }
        public RelayCommand EditTourCommand { get; private set; }
        public RelayCommand DeleteTourCommand { get; private set; }

        private readonly ITourService _tourService;
        private readonly ITourLogService _tourLogService;

        public ImageSource Map { get; set; } = new BitmapImage();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public MainViewModel(ITourService tourService, ITourLogService tourLogService) {
            _tourService = tourService;
            _tourLogService = tourLogService;
            Init();
            AddTourCommand = new(x => AddTour());
            EditTourCommand = new RelayCommand(x => EditTour());
            DeleteTourCommand = new RelayCommand(x => DeleteTour());

            new RouteService().GetRoute("Zuchering, Ingolstadt", "Ottakring, Wien").ContinueWith(task => {
                var route = task.Result;
                new MapService().GetMap(route).ContinueWith(task => {
                    Application.Current.Dispatcher.Invoke(() => {
                        Map = BitmapToImageSource(task.Result);
                        PropertyChanged?.Invoke(this, new(nameof(Map)));
                    });
                });
            });
        }

        // private BitmapImage BitmapToBitmapImage(Bitmap bitmap) {
        //     var bitmapImage = new BitmapImage();
        //     bitmapImage.BeginInit();
        //     bitmapImage.StreamSource = stream;
        //     bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //     bitmapImage.EndInit();
        //     bitmapImage.Freeze();
        //     return bitmapImage;
        // }

        private ImageSource BitmapToImageSource(Bitmap bitmap) {
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

        private void Init() {
            Tours.Clear();
            TourLogs.Clear();
            _tourService.GetAll().ForEach(tour => Tours.Add(tour));
            SelectedTour = Tours[0];
            _tourLogService.GetByTour(SelectedTour).ForEach(tourLog => TourLogs.Add(tourLog));
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
                TourLogs.Clear();
                _tourLogService.GetByTour(tour).ForEach(tourLog => TourLogs.Add(tourLog));
            }
        }

        public void AddTour() {
            new TourDialog().Show();
        }

        public void EditTour() {
            new TourDialog(SelectedTour).Show();
        }

        public void DeleteTour() {
            _tourService.Remove(SelectedTour);
            Init();
        }
    }
}
