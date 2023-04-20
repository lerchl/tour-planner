using Microsoft.Extensions.Configuration;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     Holder of the API CONFIG.
    /// </summary>
    internal class ApiConfig {

        public static readonly string MAP_QUEST = "MapQuest";

        private static readonly IConfiguration CONFIG = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public static string GetApiKey(string name) {
            return CONFIG.GetSection("ApiKeys")[name];
        }
    }
}
