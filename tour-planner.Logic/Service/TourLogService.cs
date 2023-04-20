using TourPlanner.Data.Repository;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="EntityService{E,R,V}"/> implementation for <see cref="TourLog"/>s.
    /// </summary>
    public class TourLogService : EntityService<TourLog, ITourLogRepository, TourLogValidator>, ITourLogService {

        public TourLogService() : base(DbTourLogRepository.Instance, new TourLogValidator()) {
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
