using TourPlanner.Data.Repository;
using TourPlanner.Logging;
using TourPlanner.Logic.Search;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="CrudService{E,R,V}"/> implementation for <see cref="Tour"/>s.
    /// </summary>
    public class TourService : CrudService<Tour, ITourRepository, TourValidator>, ITourService {

        private readonly ITourLogService _tourLogService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourService(ITourRepository tourRepository, ITourLogService tourLogService) :
                base(tourRepository, new TourValidator()) {
            _tourLogService = tourLogService;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public override Tour Add(Tour entity) {
            Trim(entity);
            return base.Add(entity);
        }

        public override Tour Update(Tour tour) {
            return Update(tour, true);
        }

        public Tour Update(Tour tour, bool updateLastEdited) {
            Trim(tour);

            if (updateLastEdited) {
                tour.LastEdited = DateTime.Now;
            }

            return base.Update(tour);
        }

        [LogTimeSpent(true)]
        public List<Tour> FullTextSearch(string search) {
            var tours = _repository.GetAll();

            if (search.Length == 0) {
                return tours;
            }

            var matchers = new List<IFullTextSearchMatcher> {
                new TourMatcher(),
                new TourLogMatcher(_tourLogService),
                new PopularityRankMatcher(this),
                new ChildfriendlinessMatcher(this)
            };

            return tours.Where(tour => matchers.Any(matcher => matcher.Matches(tour, search))).ToList();
        }

        [LogTimeSpent(true)]
        public int GetPopularityRank(Tour tour) {
            return _repository.GetAll()
                        // map tour to a tuple itself and it's logs
                       .Select(t => new Tuple<Tour, List<TourLog>>(t, _tourLogService.GetByTour(t)))
                        // order by number of logs
                       .OrderByDescending(t => t.Item2.Count)
                       .ToList()
                        // get index of tour and add 1 to get the rank
                        // index 0 results in rank 1 and so on
                       .FindIndex(t => t.Item1.Id == tour.Id) + 1;
        }

        [LogTimeSpent(true)]
        public double GetChildFriendliness(Tour tour) {
            var tourLogs = _tourLogService.GetByTour(tour);
            double distance = tour.Distance ?? 0;
            double time = tour.EstimatedTime ?? 0;
            double difficulty = 1;

            if (tourLogs.Any()) {
                time = tourLogs.Select(tl => tl.Duration).Average();
                difficulty = Difficulty.ALL.Count / tourLogs.Select(tl => tl.Difficulty.Value).Average();
            }

            int factor = 1;
            if (tour.TransportType == TransportType.BIKE) {
                factor = 10;
            } else if (tour.TransportType == TransportType.FASTEST || tour.TransportType == TransportType.SHORTEST) {
                factor = 100;
            }

            return Math.Round(distance * time * difficulty / factor, 2);
        }

        private static void Trim(Tour tour) {
            tour.Name = tour.Name.Trim();
            tour.Description = tour.Description.Trim();
            tour.From = tour.From.Trim();
            tour.To = tour.To.Trim();
        }
    }
}
