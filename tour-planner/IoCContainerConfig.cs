using System;
using Microsoft.Extensions.DependencyInjection;
using TourPlanner.Data.Repository;
using TourPlanner.Logic.Service;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner {

    internal class IoCContainerConfig {

        public MainViewModel MainViewModel => serviceProvider.GetRequiredService<MainViewModel>();
        public LoadingViewModel LoadingIndicatorViewModel => serviceProvider.GetRequiredService<LoadingViewModel>();

        private readonly IServiceProvider serviceProvider;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public IoCContainerConfig() {
            var services = new ServiceCollection();

            // Repositories
            services.AddSingleton<ITourRepository, DbTourRepository>();
            services.AddSingleton<ITourLogRepository, DbTourLogRepository>();

            // Services
            services.AddSingleton<ITourService, TourService>();
            services.AddSingleton<ITourLogService, TourLogService>();
            services.AddSingleton<IRouteService, RouteService>();
            services.AddSingleton<IMapService, MapService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoadingViewModel>();
            services.AddTransient<TourDialogViewModel>();
            services.AddTransient<TourLogDialogViewModel>();

            // View Services
            services.AddSingleton<IDialogService, DialogService>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
