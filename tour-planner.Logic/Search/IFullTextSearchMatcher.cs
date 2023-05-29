using TourPlanner.Model;

namespace TourPlanner.Logic.Search {

    /// <summary>
    ///     Interface for full text search matchers.
    /// </summary>
    public interface IFullTextSearchMatcher {

        /// <summary>
        ///     Checks if a <paramref name="tour"/> matches a <paramref name="search"/>.
        /// </summary>
        /// <param name="tour">The <see cref="Tour"/> to check</param>
        /// <param name="search">The search string</param>
        /// <returns>True if the <paramref name="tour"/> matches the <paramref name="search"/>, false otherwise.</returns>
        public bool Matches(Tour tour, string search);
    }
}
