using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using TourPlanner.Logic;
using TourPlanner.Logic.Port;
using TourPlanner.Logic.Report;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourListViewModel : BaseViewModel {

        private const string SEPARATOR = "\n\n";

        public ObservableCollection<Tour> Tours { get; private set; } = new();

        private Tour? _selectedTour;
        public Tour? SelectedTour {
            get => _selectedTour;
            set {
                _selectedTour = value;
                RaisePropertyChanged();
            }
        }

        public event EventHandler? TourSelected;

        private string _filterText = "";
        public string FilterText {
            get => _filterText;
            set {
                _filterText = value;
                Task.Run(() => new TourListFilterRequest(LoadTours).Filter());
                RaisePropertyChanged();
            }
        }

        public LoadingIndicatorViewModel? LoadingViewModel { get; set;}

        public RelayCommand CreateToursReportCommand { get; private set; }
        public RelayCommand ExportCommand { get; private set; }
        public RelayCommand ImportCommand { get; private set; }

        private readonly IDialogService _dialogService;
        private readonly IShowErrorService _showErrorService;
        private readonly ITourService _tourService;
        private readonly ITourLogService _tourLogService;
        private readonly ITourCSVParser _tourCSVParser;
        private readonly ITourLogCSVParser _tourLogCSVParser;

        private readonly ITourReporter<PdfDocument> _pdfTourReporter;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourListViewModel(IDialogService dialogService, IShowErrorService showErrorService, ITourService tourService, ITourLogService tourLogService, ITourCSVParser tourCSVParser, ITourLogCSVParser tourLogCSVParser) {
            _dialogService = dialogService;
            _showErrorService = showErrorService;
            _tourService = tourService;
            _tourLogService = tourLogService;
            _tourCSVParser = tourCSVParser;
            _tourLogCSVParser = tourLogCSVParser;

            CreateToursReportCommand = new(x => CreateToursReport());
            ExportCommand = new(x => Task.Run(Export));
            ImportCommand = new(x => Task.Run(Import).ContinueWith(x => LoadTours()));
            _pdfTourReporter = new PdfTourReporter(() => new PdfWriter("Tours.pdf"));
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void LoadTours() {
            Guid selected = SelectedTour?.Id ?? Guid.Empty;
            InUI(ClearTours);
            var tours = _tourService.FullTextSearch(FilterText);
            InUI(() => ShowTours(tours, selected));
        }

        private void ClearTours() {
            SelectedTour = null;
            Tours.Clear();
            LoadingViewModel?.Show();
        }

        private void ShowTours(List<Tour> tours, Guid selected) {
            LoadingViewModel?.Hide();
            tours.ForEach(Tours.Add);

            if (Tours.Any()) {
                var tour = Tours.FirstOrDefault(t => t.Id == selected);
                SelectedTour = tour ?? Tours[0];
            } else {
                SelectedTour = null;
            }

            TourSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SelectTour(Tour tour) {
            SelectedTour = tour;
            TourSelected?.Invoke(this, EventArgs.Empty);
        }

        private void CreateToursReport() {
            var tourLogs = new Dictionary<Tour, List<TourLog>>();
            Tours.ToList().ForEach(t => tourLogs.Add(t, _tourLogService.GetByTour(t)));
            _pdfTourReporter.ToursReport(Tours.ToList(), tourLogs).Close();
        }

        private void Export() {
            string date = Regex.Replace(DateUtils.FormatDateTime(DateTime.Now), "[.: ]", "_");
            using var exportStreamWriter = new StreamWriter($"Export_{date}.csv");

            exportStreamWriter.Write(_tourCSVParser.Serialize(Tours.ToList()));
            exportStreamWriter.Write(SEPARATOR);
            exportStreamWriter.Write(_tourLogCSVParser.Serialize(_tourLogService.GetAllWithTours()));
        }

        private void Import() {
            try {

                var result = _dialogService.OpenSelectImportDialog();

                // Cancelled
                if (!result.Item1) {
                    return;
                }

                using var streamReader = new StreamReader(result.Item2);
                string content = streamReader.ReadToEnd();

                string[] parts = content.Split(SEPARATOR);

                var tours = _tourCSVParser.Deserialize(parts[0]);
                tours.ForEach(t => _tourService.Add(t));

                var tourLogs = _tourLogCSVParser.Deserialize(parts[1]);
                tourLogs.ForEach(tl => _tourLogService.Add(tl));
            } catch (Exception e) {
                // TODO: Log exception
                var message = "Import failed: " + e.Message + "\nData might have been partially imported.";
                _showErrorService.ShowError(new Exception(message, e));
            }
        }
    }
}
