using System.Text.Json.Serialization;

namespace TourPlanner.Model {

    public class BoundingBox {

        [JsonPropertyName("ul")]
        public Coordinate UpperLeft { get; set; }

        [JsonPropertyName("lr")]
        public Coordinate LowerRight { get; set; }
    }
}
