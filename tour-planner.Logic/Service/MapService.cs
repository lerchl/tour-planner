using System.Drawing;
using TourPlanner.Model;
using static TourPlanner.Logic.Service.ApiConfig;

namespace TourPlanner.Logic.Service {

    public class MapService : IMapService {

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public async Task<Bitmap> GetMap(Route route) {
            var boundingBox = $"{route.BoundingBox!.UpperLeft.Latitude.ToString().Replace(',', '.')}," +
                              $"{route.BoundingBox.UpperLeft.Longitude.ToString().Replace(',', '.')}," +
                              $"{route.BoundingBox.LowerRight.Latitude.ToString().Replace(',', '.')}," +
                              $"{route.BoundingBox.LowerRight.Longitude.ToString().Replace(',', '.')}";

            var url = $"https://www.mapquestapi.com/staticmap/v5/map?" +
                    $"key={GetApiKey(MAP_QUEST)}&" +
                    $"session={route.SessionId}&" +
                    $"boundingBox={boundingBox}&" +
                    $"margin=10";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var stream = await response.Content.ReadAsStreamAsync();
            return new Bitmap(stream);
        }
    }
}
