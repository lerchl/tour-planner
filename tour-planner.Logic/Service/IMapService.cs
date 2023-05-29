using System.Drawing;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     Service for generating maps.
    /// </summary>
    public interface IMapService {

        /// <summary>
        ///     Generate a map from a <see cref="Route"/>.
        /// </summary>
        /// <param name="route">The route to generate the map from</param>
        /// <returns>The generated map</returns>
        public Task<Bitmap> GetMap(Route route);
    }
}
