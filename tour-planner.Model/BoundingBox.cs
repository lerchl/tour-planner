using System.Text.Json.Serialization;

namespace TourPlanner.Model {

    /// <summary>
    ///     Represents the bounding box of a <see cref="Route"/>.
    /// </summary>
    public class BoundingBox {

        [JsonPropertyName("ul")]
        public Coordinate UpperLeft { get; set; } = new();

        [JsonPropertyName("lr")]
        public Coordinate LowerRight { get; set; } = new();
    }
}
