using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Xml.Serialization;
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

        private readonly TourService _tourService = new();
        private readonly TourLogService _tourLogService = new();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public MainViewModel() {
            Init();
            AddTourCommand = new(x => AddTour());
            EditTourCommand = new RelayCommand(x => EditTour());
            DeleteTourCommand = new RelayCommand(x => DeleteTour());
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
