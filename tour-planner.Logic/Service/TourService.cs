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

        public override Tour Add(Tour entity) {
            Trim(entity);
            return base.Add(entity);
        }

        public override Tour Update(Tour tour) {
            Trim(tour);
            tour.LastEdited = DateTime.UtcNow;
            return base.Update(tour);
        }

        public List<Tour> GetByNameContains(string search) {
            return _repository.GetByNameContains(search);
        }

        public int GetPopularityRank(Tour tour) {
            return _repository.GetPopularityRank(tour);
        }

        private static void Trim(Tour tour) {
            tour.Name = tour.Name.Trim();
            tour.Description = tour.Description.Trim();
            tour.From = tour.From.Trim();
            tour.To = tour.To.Trim();
        }
    }
}
