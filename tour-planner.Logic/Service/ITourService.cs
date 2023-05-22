using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    public interface ITourService : ICrudService<Tour> {

        /// <summary>
        ///     Get all <see cref="Tour"/>s that contain the given string in their name.
        /// </summary>
        /// <param name="search">the string to search for</param>
        /// <returns>A list of <see cref="Tour"/>s that contain the given string in their name</returns>
        public List<Tour> GetByNameContains(string search);

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
