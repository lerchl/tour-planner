using System.Windows;
using tour_planner.ViewModels;

namespace tour_planner.Views {

    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            tours.ItemsSource = ((MainViewModel) this.DataContext).Tours;
        }
    }
}
