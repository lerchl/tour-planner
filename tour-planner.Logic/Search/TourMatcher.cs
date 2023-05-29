using TourPlanner.Model;

using static TourPlanner.Logic.Search.ComparisonUtils;

namespace TourPlanner.Logic.Search {

    /// <summary>
    ///     Matcher for the details of a <see cref="Tour"/>.
    /// </summary>
    public class TourMatcher : IFullTextSearchMatcher {

        public bool Matches(Tour tour, string search) {
            return ContainsIgnoreCase(tour.Name, search)
                    || ContainsIgnoreCase(tour.Description, search)
                    || ContainsIgnoreCase(tour.From, search)
                    || ContainsIgnoreCase(tour.To, search)
                    || ContainsIgnoreCase(tour.TransportType, search)
                    || ContainsIgnoreCase(tour.LastEdited, search)
                    || ContainsIgnoreCase(tour.LastFetched, search)
                    || ContainsIgnoreCase(tour.Distance, search)
                    || ContainsIgnoreCase(tour.EstimatedTime, search);
        }
    }
}
