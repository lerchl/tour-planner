using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourListViewModel : BaseViewModel {

        public ObservableCollection<Tour> Tours { get; private set; } = new();

        private Tour? _selectedTour;
        public Tour? SelectedTour {
            get => _selectedTour;
            set {
                _selectedTour = value;
                RaisePropertyChanged();
            }
        }

        public event EventHandler? TourSelected;

        private string _filterText = "";
        public string FilterText {
            get => _filterText;
            set {
                _filterText = value;
                Task.Run(() => new TourListFilterRequest(LoadTours).Filter());
                RaisePropertyChanged();
            }
        }

        public LoadingIndicatorViewModel? LoadingViewModel { get; set;}

        private readonly ITourService _tourService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourListViewModel(ITourService tourService) {
            _tourService = tourService;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void LoadTours() {
            Guid selected = SelectedTour?.Id ?? Guid.Empty;
            InUI(ClearTours);
            var tours = _tourService.GetByNameContains(FilterText);
            InUI(() => ShowTours(tours, selected));
        }

        private void ClearTours() {
            SelectedTour = null;
            Tours.Clear();
            LoadingViewModel?.Show();
        }

        private void ShowTours(List<Tour> tours, Guid selected) {
            LoadingViewModel?.Hide();
            tours.ForEach(Tours.Add);

            if (Tours.Any()) {
                var tour = Tours.FirstOrDefault(t => t.Id == selected);
                SelectedTour = tour ?? Tours[0];
            } else {
                SelectedTour = null;
            }

            TourSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SelectTour(Tour tour) {
            SelectedTour = tour;
            TourSelected?.Invoke(this, EventArgs.Empty);
        }
    }
}
