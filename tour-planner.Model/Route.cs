using System.Text.Json.Serialization;

namespace TourPlanner.Model {

    public class Route {

        [JsonPropertyName("sessionId")]
        public string? SessionId { get; set; }

        /// <summary>
        ///     Distance in kilometers or miles.
        /// </summary>
        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        /// <summary>
        ///     Estimated time for the tour in seconds.
        /// </summary>
        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("boundingBox")]
        public BoundingBox? BoundingBox { get; set; }
    }
}