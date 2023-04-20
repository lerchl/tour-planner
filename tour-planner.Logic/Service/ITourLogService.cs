using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    public interface ITourLogService : IEntityService<TourLog> {

        public List<TourLog> GetByTour(Tour tour);
    }
}
