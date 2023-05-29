using System.Text.Json.Serialization;

namespace TourPlanner.Model {

    /// <summary>
    ///     Represents a coordinate through Latitude and Longitude.
    ///     See also <seealso cref="BoundingBox"/>
    /// </summary>
    public class Coordinate {

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lng")]
        public double Longitude { get; set; }
    }
}
