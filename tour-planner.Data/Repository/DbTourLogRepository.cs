using Microsoft.EntityFrameworkCore;
using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     Postgre <see cref="ITourLogRepository"/> implementation.
    /// </summary>
    public class DbTourLogRepository : DbCrudRepository<DbTourLogRepository, TourLog, PostgreContext>, ITourLogRepository {

        protected override DbSet<TourLog> GetDbSet(PostgreContext context) {
            return context.TourLogs;
        }

        public List<TourLog> GetByTour(Tour tour) {
            using var context = new PostgreContext();
            return context.TourLogs.Where(tl => tl.Tour == tour).ToList();
        }
    }
}
