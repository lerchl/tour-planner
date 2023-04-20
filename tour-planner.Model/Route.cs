using System.Text.Json.Serialization;

namespace TourPlanner.Model {

    public class Route {

        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }

        [JsonPropertyName("boundingBox")]
        public BoundingBox BoundingBox { get; set; }
    }
}