using System.Drawing;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    public interface IMapService {

        public Task<Bitmap> GetMap(Route route);
    }
}
