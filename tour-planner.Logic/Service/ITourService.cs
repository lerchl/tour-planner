using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="ICrudService{E}"/> implementation for <see cref="Tour"/>s.
    /// </summary>
    public interface ITourService : ICrudService<Tour> {

        /// <summary>
        ///     Update a <see cref="Tour"/>.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/></param>
        /// <param name="updateLastEdited">whether <see cref="Tour.LastEdited"/> should be updated</param>
        /// <returns>The updated <see cref="Tour"/></returns>
        public Tour Update(Tour tour, bool updateLastEdited);

        /// <summary>
        ///     Get all <see cref="Tour"/>s that match via full text search for a string.
        /// </summary>
        /// <param name="search">the string to search for</param>
        /// <returns>A list of <see cref="Tour"/>s that match</returns>
        public List<Tour> FullTextSearch(string search);

        /// <summary>
        ///     Get the popularity rank of a <see cref="Tour"/>.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/></param>
        /// <returns>The popularity rank of the <see cref="Tour"/></returns>
        public int GetPopularityRank(Tour tour);

        /// <summary>
        ///     Get the child friendliness of a <see cref="Tour"/>.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/></param>
        /// <returns>The child friendliness of the <see cref="Tour"/></returns>
        public double GetChildFriendliness(Tour tour);
    }
}
