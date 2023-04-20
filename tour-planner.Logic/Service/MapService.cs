using TourPlanner.Model;
using static TourPlanner.Logic.Service.ApiConfig;

namespace TourPlanner.Logic.Service {

    public class MapService : IMapService {

        public async void GetMap(Route route) {
            var boundingBox = $"{route.BoundingBox.UpperLeft.Latitude}," +
                              $"{route.BoundingBox.UpperLeft.Longitude}," +
                              $"{route.BoundingBox.LowerRight.Latitude}," +
                              $"{route.BoundingBox.LowerRight.Longitude}";
            var url = $"https://www.mapquestapi.com/staticmap/v5/map?key={GetApiKey(MAP_QUEST)}&session={route.SessionId}&boundingBox={boundingBox}&margin=50";
            using var client = new HttpClient();
            var res = await client.GetAsync(url);
            var stream = await res.Content.ReadAsStreamAsync();
            // save output stream
            using var fileStream = new FileStream("map.png", FileMode.Create);
            await stream.CopyToAsync(fileStream);
            Console.WriteLine("Map saved to map.png");
        }
    }
}
