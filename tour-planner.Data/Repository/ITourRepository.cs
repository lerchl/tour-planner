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
        public ICollection<Tour> GetByNameContains(string search);
    }
}
