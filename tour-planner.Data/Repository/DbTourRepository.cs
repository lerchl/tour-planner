using Microsoft.EntityFrameworkCore;
using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     Postgre <see cref="ITourRepository"/> implementation.
    /// </summary>
    public class DbTourRepository : DbCrudRepository<Tour, PostgreContext>, ITourRepository {

        private static DbTourRepository? _instance;

        public static DbTourRepository Instance {
            get {
                _instance ??= new();
                return _instance;
            }
            private set => _instance = value;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        private DbTourRepository() {
            // noop
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

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
