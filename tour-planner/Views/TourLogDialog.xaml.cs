using System.Windows;
using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.Views {

    public partial class TourLogDialog : Window {

        public TourLogDialog(TourLogDialogViewModel tourLogDialogViewModel, TourLog tourLog)  {
            InitializeComponent();
            DataContext = tourLogDialogViewModel;
            tourLogDialogViewModel.Init(tourLog, () => Close(), dialogResult => DialogResult = dialogResult);
        }

        public TourLogDialog(TourLogDialogViewModel tourLogDialogViewModel, Tour tour) : this(tourLogDialogViewModel, new TourLog(tour)) {
            // noop
        }
    }
}
