using TourPlanner.Data.Repository;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="EntityService{E,R,V}"/> implementation for <see cref="TourLog"/>s.
    /// </summary>
    public class TourLogService : EntityService<TourLog, ITourLogRepository, TourLogValidator> {

        public TourLogService() : base(new DbTourLogRepository(), new TourLogValidator()) {
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
