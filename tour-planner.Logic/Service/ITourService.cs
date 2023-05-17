using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    public interface ITourService : ICrudService<Tour> {

        public List<Tour> GetByNameContains(string search);

        /// <summary>
        ///     Get the popularity rank of a <see cref="Tour"/>.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/></param>
        /// <returns>The popularity rank of the <see cref="Tour"/></returns>
        public int GetPopularityRank(Tour tour);
    }
}
