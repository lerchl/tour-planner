using TourPlanner.Logic.Service;
using TourPlanner.Model;

using static TourPlanner.Logic.Search.ComparisonUtils;

namespace TourPlanner.Logic.Search {

    /// <summary>
    ///     Matcher for the tour logs of a <see cref="Tour"/>.
    /// </summary>
    public class TourLogMatcher : IFullTextSearchMatcher {

        private readonly ITourLogService _tourLogService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourLogMatcher(ITourLogService tourLogService) {
            _tourLogService = tourLogService;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public bool Matches(Tour tour, string search) {
            return _tourLogService.GetByTour(tour)
                                  .Select(tourLog => MatchTourLog(tourLog, search))
                                  .Any(match => match);
        }

        private static bool MatchTourLog(TourLog tourLog, string search) {
            return ContainsIgnoreCase(tourLog.DateTime, search)
                    || ContainsIgnoreCase(tourLog.Duration, search)
                    || ContainsIgnoreCase(tourLog.Rating, search)
                    || ContainsIgnoreCase(tourLog.Difficulty, search)
                    || ContainsIgnoreCase(tourLog.Comment, search);
        }
    }
}
