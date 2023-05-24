using TourPlanner.Logic.Service;
using TourPlanner.Model;

using static TourPlanner.Logic.Search.ComparisonUtils;

namespace TourPlanner.Logic.Search {

    public abstract class SingleValueMatcher<V> : IFullTextSearchMatcher {

        protected readonly ITourService _tourService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public SingleValueMatcher(ITourService tourService) {
            _tourService = tourService;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public bool Matches(Tour tour, string search) {
            return ContainsIgnoreCase(GetValue(tour), search);
        }

        protected abstract V GetValue(Tour tour);
    }
}
