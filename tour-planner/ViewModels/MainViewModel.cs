using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    internal class MainViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Tour> Tours { get; private set; }

        private string _filterText = "";
        public string FilterText { 
            get => _filterText; 
            set {
                _filterText = value;
                PropertyChanged?.Invoke(this, new(nameof(FilterText)));
                FilterTours();
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour {
            get => _selectedTour;
            set {
                _selectedTour = value;
                PropertyChanged?.Invoke(this, new(nameof(SelectedTour)));
            }
        }

        private readonly TourService _tourService = new();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public MainViewModel() {
            Tours = new ObservableCollection<Tour>(_tourService.GetAll());
            _selectedTour = Tours[0];
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
            }
        }
    }
}
