using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourDialogViewModel : INotifyPropertyChanged {

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

        public List<TransportType> TransportTypes => TransportType.ALL.ToList();

        public Action Close { get; private set; } = () => { };
        public Action<bool> SetDialogResult { get; private set; } = (result) => { };

        private readonly ITourService _tourService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourDialogViewModel(ITourService tourService) {
            _tourService = tourService;
            SaveCommand = new RelayCommand(x => Save());
            CancelCommand = new RelayCommand(x => Cancel());
        }

        public void Init(Tour tour, Action close, Action<bool> setDialogResult) {
            Tour = tour;
            DialogTitle = tour.Id == Guid.Empty ? "Create Tour" : "Edit Tour";
            Close = close;
            SetDialogResult = setDialogResult;
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
            SetDialogResult(true);
            Close();
        }

        private void Cancel() {
            SetDialogResult(false);
            Close();
        }
    }
}
