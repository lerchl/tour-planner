using System.Text.Json;
using System.Text.Json.Nodes;
using TourPlanner.Logging;
using TourPlanner.Logic.Service.Except;
using TourPlanner.Model;
using static TourPlanner.Logic.Service.ApiConfig;
using static TourPlanner.Logic.Service.MapQuestStatusCode;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="IRouteService"/> implementation for MapQuest.
    /// </summary>
    public class RouteService : IRouteService {

        [LogTimeSpent]
        public async Task<Route> GetRoute(string from, string to, TransportType transportType) {
            var url = $"https://www.mapquestapi.com/directions/v2/route?" +
                    $"key={GetApiKey(MAP_QUEST)}&" +
                    $"from={from}&" +
                    $"to={to}&" +
                    $"unit=k&" +
                    $"routeType={transportType.Value}";

            using var client = new HttpClient();
            var res = await client.GetAsync(url);

            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                throw new InvalidKeyException();
            }

            var json = await res.Content.ReadAsStringAsync();
            var root = JsonNode.Parse(json)!;
            var route = root["route"]!;

            // Successful if no routeError is present
            if (route["routeError"] == null) {
                return JsonSerializer.Deserialize<Route>(route.ToJsonString())!;
            }

            // https://developer.mapquest.com/documentation/directions-api/status-codes/
            int statusCode = root["info"]!["statuscode"]!.GetValue<int>();

            if (statusCode == (int) INVALID_LOCATIONS) {
                throw new InvalidLocationsException(from, to);
            }

            throw new Exception($"{statusCode}: {route["info"]!["messages"]!.GetValue<string[]>()}");
        }
    }
}
