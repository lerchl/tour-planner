using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    public interface ITourLogService : ICrudService<TourLog> {

        public List<TourLog> GetByTour(Tour tour);

        public List<TourLog> GetAllWithTours();
    }
}
