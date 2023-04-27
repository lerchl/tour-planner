using TourPlanner.Model;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner {

    class DialogService : IDialogService {

        private readonly TourDialogViewModel _tourDialogViewModel;

        public DialogService(TourDialogViewModel tourDialogViewModel) {
            _tourDialogViewModel = tourDialogViewModel;
        }

        public bool OpenAddTourDialog() {
            return new TourDialog(_tourDialogViewModel).ShowDialog() == true;
        }

        public bool OpenEditTourDialog(Tour tour) {
            return new TourDialog(_tourDialogViewModel, tour).ShowDialog() == true;
        }
    }
}
