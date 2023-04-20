using System;
using Microsoft.Extensions.DependencyInjection;
using TourPlanner.Data.Repository;
using TourPlanner.ViewModels;

namespace TourPlanner {

    internal class IoCContainerConfig {

        public MainViewModel MainViewModel => serviceProvider.GetRequiredService<MainViewModel>();

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


            // ViewModels
            services.AddSingleton<MainViewModel>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
