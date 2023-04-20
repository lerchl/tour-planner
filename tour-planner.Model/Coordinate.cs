using System.Text.Json.Serialization;

namespace TourPlanner.Model {

    public class Coordinate {

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lng")]
        public double Longitude { get; set; }
    }
}
