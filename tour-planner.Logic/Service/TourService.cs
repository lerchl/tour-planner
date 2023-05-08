using TourPlanner.Data.Repository;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="CrudService{E,R,V}"/> implementation for <see cref="Tour"/>s.
    /// </summary>
    public class TourService : CrudService<Tour, ITourRepository, TourValidator>, ITourService {

        public TourService(ITourRepository tourRepository) :
                base(tourRepository, new TourValidator()) {
            // noop
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public override Tour Update(Tour tour) {
            tour.LastEdited = DateTime.UtcNow;
            return _repository.Update(tour);
        }

        public List<Tour> GetByNameContains(string search) {
            return _repository.GetByNameContains(search);
        }

        public int GetPopularityRank(Tour tour) {
            return _repository.GetPopularityRank(tour);
        }
    }
}
