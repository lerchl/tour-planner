using System.Windows;
using System.Windows.Controls;
using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.Views {

    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private void TourSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Tour tour) {
                ((MainViewModel) DataContext).SelectTour(tour);
            }
        }

        private void TourLogSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TourLog tourLog) {
                ((MainViewModel) DataContext).SelectTourLog(tourLog);
            }
        }
    }
}
