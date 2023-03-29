using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     Interface for <see cref="TourLog"/> repository.
    /// </summary>
    public interface ITourLogRepository {

        /// <summary>
        ///     Get all <see cref="TourLog"/>s for a <see cref="Tour"/>.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/></param>
        /// <returns>all <see cref="TourLog"/>s for the <see cref="Tour"/></returns>
        ICollection<TourLog> GetByTour(Tour tour);

        /// <summary>
        ///     Add a <see cref="TourLog"/> to the database.
        /// </summary>
        /// <param name="tourLog">the <see cref="TourLog"/> to add</param>
        /// <returns>The added <see cref="TourLog"/></returns>
        TourLog Add(TourLog tourLog);

        /// <summary>
        ///     Update a <see cref="TourLog"/> in the database.
        /// </summary>
        /// <param name="tourLog">the <see cref="TourLog"/> to update</param>
        /// <returns>The updated <see cref="TourLog"/></returns>
        TourLog Update(TourLog tourLog);

        /// <summary>
        ///     Remove a <see cref="TourLog"/> from the database.
        /// </summary>
        /// <param name="tourLog">the <see cref="TourLog"/> to remove</param>
        void Remove(TourLog tourLog);
    }

    /// <summary>
    ///     <see cref="ITourLogRepository"/> implementation.
    /// </summary>
    public class TourLogRepository : Singleton<TourLogRepository>, ITourLogRepository {

        public ICollection<TourLog> GetByTour(Tour tour) {
            using var context = new PostgreContext();
            return context.TourLogs.Where(tl => tl.Tour == tour).ToList();
        }

        public TourLog Add(TourLog tourLog) {
            using var context = new PostgreContext();
            context.TourLogs.Add(tourLog);
            context.SaveChanges();
            return tourLog;
        }

        public TourLog Update(TourLog tourLog) {
            using var context = new PostgreContext();
            context.TourLogs.Update(tourLog);
            context.SaveChanges();
            return tourLog;
        }

        public void Remove(TourLog tourLog) {
            using var context = new PostgreContext();
            context.TourLogs.Remove(tourLog);
            context.SaveChanges();
        }
    }
}
