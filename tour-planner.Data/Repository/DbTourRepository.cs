using Microsoft.EntityFrameworkCore;
using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     Postgre <see cref="ITourRepository"/> implementation.
    /// </summary>
    public class DbTourRepository : DbCrudRepository<Tour, PostgreContext>, ITourRepository {

        protected override DbSet<Tour> GetDbSet(PostgreContext context) {
            return context.Tours;
        }

        public List<Tour> GetByNameContains(string search) {
            using var context = new PostgreContext();
            // only saved tours can be searched for
            // and name is required in the database
            bool nameMatches(Tour t) => t.Name!.ToLower().Contains(search.ToLower());
            return GetDbSet(context).Where(nameMatches).ToList();
        }

        public int GetPopularityRank(Tour tour) {
            using var context = new PostgreContext();
            return GetDbSet(context).Include(t => t.TourLogs)
                                    .OrderByDescending(t => t.TourLogs.Count)
                                    .ToList()
                                    .FindIndex(t => t.Id == tour.Id) + 1;
        }
    }
}
