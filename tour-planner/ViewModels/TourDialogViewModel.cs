using System;
using TourPlanner.Logic.Service;

namespace TourPlanner.ViewModels {

    internal class TourDialogViewModel {

        public string DialogTitle { get; private set; } = "";
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private TourService _tourService = new();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourDialogViewModel() {
            SaveCommand = new RelayCommand(x => Save());
            CancelCommand = new RelayCommand(x => Cancel());
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////
    
        // TODO: find best approach for closing dialog https://stackoverflow.com/questions/4376475/wpf-mvvm-how-to-close-a-window

        private void Save() {

        }

        private void Cancel() {

        }
    }
}
