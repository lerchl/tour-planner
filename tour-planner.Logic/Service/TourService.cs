using TourPlanner.Data.Repository;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="EntityService{E,R,V}"/> implementation for <see cref="Tour"/>s.
    /// </summary>
    public class TourService : EntityService<Tour, ITourRepository, TourValidator> {

        public TourService() : base(DbTourRepository.Instance, new TourValidator()) {
            // noop
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public List<Tour> GetByNameContains(string search) {
            return _repository.GetByNameContains(search);
        }
    }
}
