using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="ICrudService{E}"/> implementation for <see cref="TourLog"/>s.
    /// </summary>
    public interface ITourLogService : ICrudService<TourLog> {

        /// <summary>
        ///     Get all <see cref="TourLog"/>s for a <see cref="Tour"/>.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/></param>
        /// <returns>all <see cref="TourLog"/>s for the <see cref="Tour"/></returns>
        public List<TourLog> GetByTour(Tour tour);

        /// <summary>
        ///     Get all <see cref="TourLog"/>s with their <see cref="Tour"/>s.
        /// </summary>
        /// <returns>all <see cref="TourLog"/>s with their <see cref="Tour"/>s</returns>
        public List<TourLog> GetAllWithTours();
    }
}
