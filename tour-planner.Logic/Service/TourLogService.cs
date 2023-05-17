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

        public override TourLog Add(TourLog tourLog) {
            tourLog.Comment = tourLog.Comment.Trim();
            return base.Add(tourLog);
        }

        public override TourLog Update(TourLog tourLog) {
            tourLog.Comment = tourLog.Comment.Trim();
            return base.Update(tourLog);
        }

        public List<TourLog> GetByTour(Tour tour) {
            return _repository.GetByTour(tour);
        }
    }
}
