using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     <see cref="ICrudRepository{E}"/> for <see cref="TourLog"/>s.
    /// </summary>
    public interface ITourLogRepository : ICrudRepository<TourLog> {

        /// <summary>
        ///     Get all <see cref="TourLog"/>s for a <see cref="Tour"/>.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/></param>
        /// <returns>all <see cref="TourLog"/>s for the <see cref="Tour"/></returns>
        ICollection<TourLog> GetByTour(Tour tour);
    }
}