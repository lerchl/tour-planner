using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    internal class MainViewModel : BaseViewModel {

        private readonly TourListViewModel _tourListViewModel;
        private readonly TourActionRowViewModel _tourActionRowViewModel;
        private readonly TourDetailsViewModel _tourDetailsViewModel;
        private readonly TourLogTableViewModel _tourLogTableViewModel;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public MainViewModel(TourListViewModel tourListViewModel,
                             TourActionRowViewModel tourActionRowViewModel,
                             TourDetailsViewModel tourDetailsViewModel,
                             TourLogTableViewModel tourLogTableViewModel) {
            _tourListViewModel = tourListViewModel;
            _tourActionRowViewModel = tourActionRowViewModel;
            _tourDetailsViewModel = tourDetailsViewModel;
            _tourLogTableViewModel = tourLogTableViewModel;

            _tourListViewModel.TourSelected += (sender, args) => Task.Run(SelectTour);
            _tourActionRowViewModel.OnAction += (sender, args) => Task.Run(_tourListViewModel.LoadTours);
            _tourDetailsViewModel.RouteFetched += (sender, args) => UpdateSelectedTour();

            Task.Run(() => _tourListViewModel.LoadTours());
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        private void SelectTour() {
            if (_tourListViewModel.SelectedTour == null) {
                return;
            }

            _tourActionRowViewModel.Tour = _tourListViewModel.SelectedTour;
            _tourDetailsViewModel.LoadTourDetails(_tourListViewModel.SelectedTour);
            _tourLogTableViewModel.LoadTourLogs(_tourListViewModel.SelectedTour);
        }

        private void UpdateSelectedTour() {
            int index = _tourListViewModel.Tours.IndexOf(_tourListViewModel.SelectedTour!);
            _tourListViewModel.Tours[index] = _tourDetailsViewModel.Tour!;
            _tourListViewModel.SelectedTour = _tourDetailsViewModel.Tour!;
        }
    }
}
