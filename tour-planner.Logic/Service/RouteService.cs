using System.Text.Json;
using System.Text.Json.Nodes;
using TourPlanner.Model;
using static TourPlanner.Logic.Service.ApiConfig;

namespace TourPlanner.Logic.Service {

    public class RouteService : IRouteService {

        public async Task<Route> GetRoute(string from, string to) {
            var url = $"https://www.mapquestapi.com/directions/v2/route?key={GetApiKey(MAP_QUEST)}&from={from}&to={to}";
            using var client = new HttpClient();
            var res = await client.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();
            var route = JsonSerializer.Deserialize<Route>(JsonNode.Parse(json)["route"].ToJsonString());
            return route;
        }
    }
}
