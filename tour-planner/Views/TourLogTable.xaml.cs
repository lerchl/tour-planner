using System.Windows.Controls;
using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.Views {

    public partial class TourLogTable : UserControl {

        public TourLogTable() {
            InitializeComponent();
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        private void TourLogSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TourLog tourLog) {
                ((TourLogTableViewModel) DataContext).SelectedTourLog = tourLog;
            }
        }
    }
}
