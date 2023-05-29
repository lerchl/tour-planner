using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     Service for generating routes.
    /// </summary>
    public interface IRouteService {

        /// <summary>
        ///     Generate a route.
        /// </summary>
        /// <param name="from">The starting point of the route</param>
        /// <param name="to">The end point of the route</param>
        /// <param name="transportType">The transport type to use</param>
        /// <returns>The generated route</returns>
        public Task<Route> GetRoute(string from, string to, TransportType transportType);
    }
}
