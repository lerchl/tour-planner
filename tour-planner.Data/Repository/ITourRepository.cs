using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     <see cref="ICrudRepository{E}"/> for <see cref="Tour"/>s.
    /// </summary>
    public interface ITourRepository : ICrudRepository<Tour> {

        /// <summary>
        ///     Get all <see cref="Tour"/>s whose name
        ///     contains (case insensitive) a search string.
        /// </summary>
        /// <param name="search">the search string</param>
        /// <returns>The matching <see cref="Tour"/>s</returns>
        public List<Tour> GetByNameContains(string search);
    
        /// <summary>
        ///     Get the popularity rank of a <see cref="Tour"/>.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/></param>
        /// <returns>The popularity rank of the <see cref="Tour"/></returns>
        public int GetPopularityRank(Tour tour);
    }
}
