using System;
using iText.Kernel.Pdf;
using TourPlanner.Logic.Report;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourActionRowViewModel : BaseViewModel {

        private Tour? _tour;
        public Tour? Tour {
            get => _tour;
            set {
                _tour = value;
                InUI(() => {
                    CreateTourReportCommand.RaiseCanExecuteChanged();
                    EditTourCommand.RaiseCanExecuteChanged();
                    DeleteTourCommand.RaiseCanExecuteChanged();
                });
            }
        }

        public EventHandler? OnAction;

        public RelayCommand AddTourCommand { get; private set; }
        public RelayCommand CreateTourReportCommand { get; private set; }
        public RelayCommand EditTourCommand { get; private set; }
        public RelayCommand DeleteTourCommand { get; private set; }

        private readonly IDialogService _dialogService;
        private readonly ITourService _tourService;
        private readonly ITourLogService _tourLogService;

        private readonly ITourReporter<PdfDocument> _pdfTourReporter;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourActionRowViewModel(IDialogService dialogService, ITourService tourService, ITourLogService tourLogService) {
            _dialogService = dialogService;
            _tourService = tourService;
            _tourLogService = tourLogService;

            _pdfTourReporter = new PdfTourReporter(() => new PdfWriter(Tour!.Name + ".pdf"));

            AddTourCommand = new(x => AddTour());
            CreateTourReportCommand = new RelayCommand(x => CreateTourReport(), x => Tour != null);
            EditTourCommand = new RelayCommand(x => EditTour(), x => Tour != null);
            DeleteTourCommand = new RelayCommand(x => DeleteTour(), x => Tour != null);
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        private void AddTour() {
            if (_dialogService.OpenAddTourDialog()) {
                OnAction?.Invoke(this, EventArgs.Empty);
            }
        }

        private void CreateTourReport() {
            // command is only enabled if tour is not null
            _pdfTourReporter.TourReport(Tour!, _tourLogService.GetByTour(Tour!)).Close();
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
