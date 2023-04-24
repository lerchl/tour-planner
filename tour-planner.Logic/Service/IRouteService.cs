using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    public interface IRouteService {

        public Task<Route> GetRoute(string from, string to, TransportType transportType);
    }
}
