using System;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourActionRowViewModel : BaseViewModel {

        private Tour? _tour;
        public Tour? Tour {
            get => _tour;
            set {
                _tour = value;
                RaisePropertyChanged();
            }
        }
        public EventHandler? OnAction;

        public RelayCommand AddTourCommand { get; private set; }
        public RelayCommand EditTourCommand { get; private set; }
        public RelayCommand DeleteTourCommand { get; private set; }

        private readonly IDialogService _dialogService;
        private readonly ITourService _tourService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourActionRowViewModel(IDialogService dialogService, ITourService tourService) {
            _dialogService = dialogService;
            _tourService = tourService;

            AddTourCommand = new(x => AddTour());
            // TODO: edit and delete command should be disabled if tour is null
            EditTourCommand = new RelayCommand(x => EditTour());
            DeleteTourCommand = new RelayCommand(x => DeleteTour());
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        private void AddTour() {
            if (_dialogService.OpenAddTourDialog()) {
                OnAction?.Invoke(this, EventArgs.Empty);
            }
        }

        private void EditTour() {
            // command is only enabled if tour is not null
            if (_dialogService.OpenEditTourDialog(new(Tour!))) {
                OnAction?.Invoke(this, EventArgs.Empty);
            }
        }

        private void DeleteTour() {
            // command is only enabled if tour is not null
            _tourService.Remove(Tour!);
            OnAction?.Invoke(this, EventArgs.Empty);
        }
    }
}
