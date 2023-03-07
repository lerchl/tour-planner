using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using tour_planner.ViewModels;
using tour_planner.Views;

namespace tour_planner {

    public partial class App : Application {

        private void Application_Startup(object sender, StartupEventArgs e) {
            var mainWindow = new MainWindow {
                DataContext = new MainViewModel()
            };
            mainWindow.Show();
        }
    }
}
