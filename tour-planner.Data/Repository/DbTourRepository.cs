using Microsoft.EntityFrameworkCore;
using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     Postgre <see cref="ITourRepository"/> implementation.
    /// </summary>
    public class DbTourRepository : DbCrudRepository<Tour, PostgreContext>, ITourRepository {

        protected override DbSet<Tour> GetDbSet(PostgreContext context) {
            return context.Tours!;
        }
    }
}
