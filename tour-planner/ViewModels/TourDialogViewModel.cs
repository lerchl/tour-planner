using System;
using System.ComponentModel;
using System.Reflection.Metadata;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    internal class TourDialogViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private Tour _tour = new();
        public Tour Tour {
            get => _tour;
            private set {
                _tour = value;
                PropertyChanged?.Invoke(this, new(nameof(Tour)));
            }
        }

        private string _dialogTitle = "";
        public string DialogTitle {
            get => _dialogTitle;
            private set {
                _dialogTitle = value;
                PropertyChanged?.Invoke(this, new(nameof(DialogTitle)));
            }
        }

        public Action Close { get; private set; } = () => { };

        private readonly TourService _tourService = new();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourDialogViewModel() {
            SaveCommand = new RelayCommand(x => Save());
            CancelCommand = new RelayCommand(x => Cancel());
        }

        public void Init(Tour tour, Action close) {
            Tour = tour;
            DialogTitle = tour.Id == Guid.Empty ? "Create Tour" : "Edit Tour";
            Close = close;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        private void Save() {
            if (Tour.Id == Guid.Empty) {
                _tourService.Add(Tour);
            } else {
                _tourService.Update(Tour);
            }
            Close();
        }

        private void Cancel() {
            Close();
        }
    }
}
