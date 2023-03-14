using System.Windows;
using System.Windows.Controls;
using tour_planner.ViewModels;

namespace tour_planner.Views {

    public partial class MainWindow : Window {

        private readonly MainViewModel? _viewModel;

        public MainWindow() {
            InitializeComponent();
            _viewModel = (MainViewModel) this.DataContext;
            //tours.ItemsSource = _viewModel.Tours;
        }
    }
}
