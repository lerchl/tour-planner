using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Data;
using TourPlanner.Logic.Service;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner {

    public partial class App : Application {

        private void Application_Startup(object sender, StartupEventArgs seas) {
            new RouteService().GetRoute("Vienna", "Graz");

            using (var context = new PostgreContext()) {
                try {
                    // context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }

            var ioCConfig = (IoCContainerConfig) Current.Resources["IoCConfig"];

            var mainWindow = new MainWindow {
                DataContext = ioCConfig.MainViewModel
            };
            mainWindow.Show();
        }
    }
}
