﻿using System;
using System.Windows;
using TourPlanner.Data;
using TourPlanner.Logging;
using TourPlanner.Views;

namespace TourPlanner {

    public partial class App : Application {

        private static readonly ILogger _logger = LoggerFactory.GetLogger<App>();

        private void Application_Startup(object sender, StartupEventArgs seas) {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            using (var context = new PostgreContext()) {
                try {
                    context.Database.EnsureCreated();
                } catch (Exception e) {
                    _logger.Fatal(e.Message);
                }
            }

            var ioCConfig = (IoCContainerConfig) Current.Resources["IoCConfig"];

            var tourListViewModel = ioCConfig.TourListViewModel;
            tourListViewModel.LoadingViewModel = ioCConfig.LoadingIndicatorViewModel;

            var tourDetailsViewModel = ioCConfig.TourDetailsViewModel;
            tourDetailsViewModel.MapLoadingViewModel = ioCConfig.LoadingIndicatorViewModel;
            tourDetailsViewModel.DistanceLoadingViewModel = ioCConfig.LoadingIndicatorViewModel;
            tourDetailsViewModel.EstimatedTimeLoadingViewModel = ioCConfig.LoadingIndicatorViewModel;
            tourDetailsViewModel.PopularityRankLoadingViewModel = ioCConfig.LoadingIndicatorViewModel;
            tourDetailsViewModel.ChildFriendlinessLoadingViewModel = ioCConfig.LoadingIndicatorViewModel;

            var tourLogTableViewModel = ioCConfig.TourLogTableViewModel;
            tourLogTableViewModel.LoadingViewModel = ioCConfig.LoadingIndicatorViewModel;

            var mainWindow = new MainWindow {
                DataContext = ioCConfig.MainViewModel,
                TourList = { DataContext = tourListViewModel, LoadingIndicator = { DataContext = tourListViewModel.LoadingViewModel } },
                TourActionRow = { DataContext = ioCConfig.TourActionRowViewModel },
                TourDetails = { DataContext = tourDetailsViewModel, PopularityRankLoadingIndicator = { DataContext = tourDetailsViewModel.PopularityRankLoadingViewModel },
                                                                    ChildFriendlinessLoadingIndicator = { DataContext = tourDetailsViewModel.ChildFriendlinessLoadingViewModel },
                                                                    DistanceLoadingIndicator = { DataContext = tourDetailsViewModel.DistanceLoadingViewModel },
                                                                    EstimatedTimeLoadingIndicator = { DataContext = tourDetailsViewModel.EstimatedTimeLoadingViewModel },
                                                                    MapLoadingIndicator = { DataContext = tourDetailsViewModel.MapLoadingViewModel } },
                TourLogTable = { DataContext = tourLogTableViewModel, LoadingIndicator = { DataContext = tourLogTableViewModel.LoadingViewModel } },
            };

            mainWindow.Show();
        }
    }
}
