using System;
using Microsoft.Extensions.DependencyInjection;
using TourPlanner.Data.Repository;
using TourPlanner.Logic.Service;
using TourPlanner.ViewModels;

namespace TourPlanner {

    internal class IoCContainerConfig {

        public MainViewModel MainViewModel => serviceProvider.GetRequiredService<MainViewModel>();
        public TourListViewModel TourListViewModel => serviceProvider.GetRequiredService<TourListViewModel>();
        public TourActionRowViewModel TourActionRowViewModel => serviceProvider.GetRequiredService<TourActionRowViewModel>();
        public TourDetailsViewModel TourDetailsViewModel => serviceProvider.GetRequiredService<TourDetailsViewModel>();
        public TourLogTableViewModel TourLogTableViewModel => serviceProvider.GetRequiredService<TourLogTableViewModel>();
        public LoadingIndicatorViewModel LoadingIndicatorViewModel => serviceProvider.GetRequiredService<LoadingIndicatorViewModel>();

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
            services.AddSingleton<TourListViewModel>();
            services.AddSingleton<TourActionRowViewModel>();
            services.AddSingleton<TourDetailsViewModel>();
            services.AddSingleton<TourLogTableViewModel>();

            services.AddTransient<LoadingIndicatorViewModel>();
            services.AddTransient<TourDialogViewModel>();
            services.AddTransient<TourLogDialogViewModel>();

            // View Services
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IShowErrorService, ShowErrorService>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
