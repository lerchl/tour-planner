using System.Windows;
using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.Views {

    public partial class TourDialog : Window {

        public TourDialog(TourDialogViewModel tourDialogViewModel, Tour tour)  {
            InitializeComponent();
            DataContext = tourDialogViewModel;
            tourDialogViewModel.Init(tour, () => Close(), dialogResult => DialogResult = dialogResult);
        }

        public TourDialog(TourDialogViewModel tourDialogViewModel) : this(tourDialogViewModel, new()) {
            // noop
        }
    }
}
