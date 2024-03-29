using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourLogTableViewModel : BaseViewModel {

        public ObservableCollection<TourLog> TourLogs { get; private set; } = new();

        private Tour? _tour;
        public Tour? Tour {
            get => _tour;
            set {
                _tour = value;
                InUI(AddTourLogCommand.RaiseCanExecuteChanged);
            }
        }

        private TourLog? _selectedTourLog;
        public TourLog? SelectedTourLog {
            get => _selectedTourLog;
            set {
                _selectedTourLog = value;
                RaisePropertyChanged();
                InUI(() => {
                    EditTourLogCommand.RaiseCanExecuteChanged();
                    DeleteTourLogCommand.RaiseCanExecuteChanged();
                });
            }
        }

        public EventHandler? OnAction;

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

            AddTourLogCommand = new RelayCommand(x => AddTourLog(), x => Tour != null);
            EditTourLogCommand = new RelayCommand(x => EditTourLog(), x => SelectedTourLog != null);
            DeleteTourLogCommand = new RelayCommand(x => DeleteTourLog(), x => SelectedTourLog != null);
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void LoadTourLogs(Tour? tour) {
            Tour = tour;
            InUI(() => ClearTourLogs(Tour != null));

            if (Tour != null) {
                var tourLogs = _tourLogService.GetByTour(Tour);
                InUI(() => ShowTourLogs(tourLogs));
            }
        }

        private void ClearTourLogs(bool showLoading = false) {
            SelectedTourLog = null;
            TourLogs.Clear();

            if (showLoading) {
                LoadingViewModel?.Show();
            }
        }

        private void ShowTourLogs(List<TourLog> tourLogs) {
            LoadingViewModel?.Hide();
            tourLogs.ForEach(TourLogs.Add);
        }

        private void AddTourLog() {
            // command is only enabled if tour is not null
            if (_dialogService.OpenAddTourLogDialog(Tour!)) {
                Task.Run(() => LoadTourLogs(Tour!));
                OnAction?.Invoke(this, EventArgs.Empty);
            }
        }

        private void EditTourLog() {
            // command is only enabled if tour log is not null
            if (_dialogService.OpenEditTourLogDialog(new(SelectedTourLog!))) {
                Task.Run(() => LoadTourLogs(Tour!));
                OnAction?.Invoke(this, EventArgs.Empty);
            }
        }

        private void DeleteTourLog() {
            // command is only enabled if tour log is not null
            _tourLogService.Remove(SelectedTourLog!);
            Task.Run(() => LoadTourLogs(Tour!));
            OnAction?.Invoke(this, EventArgs.Empty);
        }
    }
}
