using TourPlanner.Data.Repository;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="CrudService{E,R,V}"/> implementation for <see cref="TourLog"/>s.
    /// </summary>
    public class TourLogService : CrudService<TourLog, ITourLogRepository, TourLogValidator>, ITourLogService {

        public TourLogService(ITourLogRepository tourLogRepository) :
                base(tourLogRepository, new TourLogValidator()) {
            // noop
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public List<TourLog> GetByTour(Tour tour) {
            return _repository.GetByTour(tour);
        }
    }
}
