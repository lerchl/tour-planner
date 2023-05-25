using System;
using TourPlanner.Model;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner {

    class DialogService : IDialogService {

        private readonly TourDialogViewModel _tourDialogViewModel;
        private readonly TourLogDialogViewModel _tourLogDialogViewModel;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public DialogService(TourDialogViewModel tourDialogViewModel, TourLogDialogViewModel tourLogDialogViewModel) {
            _tourDialogViewModel = tourDialogViewModel;
            _tourLogDialogViewModel = tourLogDialogViewModel;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public bool OpenAddTourDialog() {
            return new TourDialog(_tourDialogViewModel).ShowDialog() == true;
        }

        public bool OpenEditTourDialog(Tour tour) {
            return new TourDialog(_tourDialogViewModel, tour).ShowDialog() == true;
        }

        public bool OpenAddTourLogDialog(Tour tour) {
            return new TourLogDialog(_tourLogDialogViewModel, tour).ShowDialog() == true;
        }

        public bool OpenEditTourLogDialog(TourLog tourLog) {
            return new TourLogDialog(_tourLogDialogViewModel, tourLog).ShowDialog() == true;
        }

        public Tuple<bool, string> OpenSelectImportDialog() {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            return new(dialog.ShowDialog() == true, dialog.FileName);
        }
    }
}
