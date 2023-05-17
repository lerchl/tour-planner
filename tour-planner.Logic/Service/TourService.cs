using TourPlanner.Data.Repository;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="CrudService{E,R,V}"/> implementation for <see cref="Tour"/>s.
    /// </summary>
    public class TourService : CrudService<Tour, ITourRepository, TourValidator>, ITourService {

        private readonly ITourLogRepository _tourLogRepository;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourService(ITourRepository tourRepository, ITourLogRepository tourLogRepository) :
                base(tourRepository, new TourValidator()) {
            _tourLogRepository = tourLogRepository;
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
            return _repository.GetAll()
                        // map tour to a tuple itself and it's logs
                       .Select(t => new Tuple<Tour, List<TourLog>>(t, _tourLogRepository.GetByTour(t)))
                        // order by number of logs
                       .OrderByDescending(t => t.Item2.Count)
                       .ToList()
                        // get index of tour and add 1 to get the rank
                        // index 0 results in rank 1 and so on
                       .FindIndex(t => t.Item1.Id == tour.Id) + 1;
        }

        private static void Trim(Tour tour) {
            tour.Name = tour.Name.Trim();
            tour.Description = tour.Description.Trim();
            tour.From = tour.From.Trim();
            tour.To = tour.To.Trim();
        }
    }
}
