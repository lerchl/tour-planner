using System;
using System.Windows;
using TourPlanner.Data;
using TourPlanner.Views;

namespace TourPlanner {

    public partial class App : Application {

        private void Application_Startup(object sender, StartupEventArgs seas) {
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
