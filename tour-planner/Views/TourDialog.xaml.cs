using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
