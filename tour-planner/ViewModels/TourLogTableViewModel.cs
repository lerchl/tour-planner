using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourLogTableViewModel : BaseViewModel {

        public ObservableCollection<TourLog> TourLogs { get; private set; } = new();

        public Tour? Tour { get; set; }

        private TourLog? _selectedTourLog;
        public TourLog? SelectedTourLog {
            get => _selectedTourLog;
            set {
                _selectedTourLog = value;
                RaisePropertyChanged();
            }
        }
        public event EventHandler? TourLogSelected;

        public RelayCommand AddTourLogCommand { get; private set; }
        public RelayCommand EditTourLogCommand { get; private set; }
        public RelayCommand DeleteTourLogCommand { get; private set; }

        public LoadingIndicatorViewModel? LoadingViewModel { get; set;}

        private readonly IDialogService _dialogService;
        private readonly ITourLogService _tourLogService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourLogTableViewModel(IDialogService dialogService, ITourLogService tourLogService) {
            _dialogService = dialogService;
            _tourLogService = tourLogService;

            AddTourLogCommand = new RelayCommand(x => AddTourLog());
            EditTourLogCommand = new RelayCommand(x => EditTourLog());
            DeleteTourLogCommand = new RelayCommand(x => DeleteTourLog());
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void LoadTourLogs(Tour tour) {
            Tour = tour;
            InUI(ClearTourLogs);
            var tourLogs = _tourLogService.GetByTour(tour);
            InUI(() => ShowTourLogs(tourLogs));
        }

        private void ClearTourLogs() {
            SelectedTourLog = null;
            TourLogs.Clear();
            LoadingViewModel?.Show();
        }

        private void ShowTourLogs(List<TourLog> tourLogs) {
            LoadingViewModel?.Hide();
            tourLogs.ForEach(TourLogs.Add);
        }

        private void AddTourLog() {
            // command is only enabled if tour is not null
            if (_dialogService.OpenAddTourLogDialog(Tour!)) {
                Task.Run(() => LoadTourLogs(Tour!));
            }
        }

        private void EditTourLog() {
            // command is only enabled if tour log is not null
            if (_dialogService.OpenEditTourLogDialog(SelectedTourLog!)) {
                Task.Run(() => LoadTourLogs(Tour!));
            }
        }

        private void DeleteTourLog() {
            // command is only enabled if tour log is not null
            _tourLogService.Remove(SelectedTourLog!);
            Task.Run(() => LoadTourLogs(Tour!));
        }
    }
}
