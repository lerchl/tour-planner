using Microsoft.EntityFrameworkCore;
using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     Postgre <see cref="ITourRepository"/> implementation.
    /// </summary>
    public class DbTourRepository : DbCrudRepository<DbTourRepository, Tour, PostgreContext>, ITourRepository {

        protected override DbSet<Tour> GetDbSet(PostgreContext context) {
            return context.Tours;
        }

        public List<Tour> GetByNameContains(string search) {
            using var context = new PostgreContext();
            // only saved tours can be searched for
            // and name is required in the database
            return context.Tours.Where(t => t.Name!.ToLower().Contains(search.ToLower())).ToList();
        }
    }
}
