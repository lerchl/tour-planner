using TourPlanner.Logic.Service;
using TourPlanner.Model;

using static TourPlanner.Logic.Search.ComparisonUtils;

namespace TourPlanner.Logic.Search {

    /// <summary>
    ///     Base class for full text search matchers that match against a single value.
    /// </summary>
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

        /// <summary>
        ///     Gets the value to match against.
        /// </summary>
        /// <param name="tour">The <see cref="Tour"/> to get the value from</param>
        /// <returns>The value to match against</returns>
        protected abstract V GetValue(Tour tour);
    }
}
