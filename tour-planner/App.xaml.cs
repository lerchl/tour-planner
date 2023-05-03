using System;
using System.Windows;
using TourPlanner.Data;
using TourPlanner.Views;

namespace TourPlanner {

    public partial class App : Application {

        private void Application_Startup(object sender, StartupEventArgs seas) {
            using (var context = new PostgreContext()) {
                try {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }

            var ioCConfig = (IoCContainerConfig) Current.Resources["IoCConfig"];

            var mainViewModel = ioCConfig.MainViewModel;
            mainViewModel.MapLoadingViewModel = ioCConfig.LoadingIndicatorViewModel;
            mainViewModel.DistanceLoadingViewModel = ioCConfig.LoadingIndicatorViewModel;
            mainViewModel.EstimatedTimeLoadingViewModel = ioCConfig.LoadingIndicatorViewModel;

            var mainWindow = new MainWindow {
                DataContext = mainViewModel,
                MapLoadingIndicator = { DataContext = mainViewModel.MapLoadingViewModel },
                DistanceLoadingIndicator = { DataContext = mainViewModel.DistanceLoadingViewModel },
                EstimatedTimeLoadingIndicator = { DataContext = mainViewModel.EstimatedTimeLoadingViewModel }
            };

            mainWindow.Show();
        }
    }
}
