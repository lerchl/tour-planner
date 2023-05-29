using Microsoft.EntityFrameworkCore;
using TourPlanner.Logging;
using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     Postgre <see cref="ITourLogRepository"/> implementation.
    /// </summary>
    public class DbTourLogRepository : DbCrudRepository<TourLog, PostgreContext>, ITourLogRepository {

        protected override DbSet<TourLog> GetDbSet(PostgreContext context) {
            return context.TourLogs!;
        }

        [LogTimeSpent]
        public override TourLog Add(TourLog entity) {
            using var context = new PostgreContext();
            context.Entry(entity.Tour!).State = EntityState.Unchanged;
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }

        [LogTimeSpent]
        public override TourLog Update(TourLog entity) {
            using var context = new PostgreContext();
            context.Entry(entity.Tour!).State = EntityState.Unchanged;
            context.Update(entity);
            context.SaveChanges();
            return entity;
        }

        [LogTimeSpent]
        public List<TourLog> GetByTour(Tour tour) {
            using var context = new PostgreContext();
            return GetDbSet(context).Where(tl => tl.Tour == tour).Include(tourLog => tourLog.Tour).ToList();
        }

        [LogTimeSpent]
        public List<TourLog> GetAllWithTours() {
            using var context = new PostgreContext();
            return GetDbSet(context).Include(tourLog => tourLog.Tour).ToList();
        }
    }
}
