using System.Windows.Controls;
using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.Views {

    public partial class TourList : UserControl {

        public TourList() {
            InitializeComponent();
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        private void SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Tour tour) {
                ((TourListViewModel) DataContext).SelectTour(tour);
            }
        }
    }
}
