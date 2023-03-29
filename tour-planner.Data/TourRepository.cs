using TourPlanner.Model;

namespace TourPlanner.Data {

    /// <summary>
    /// Interface for <see cref="Tour"/> repository.
    /// </summary>
    public interface ITourRepository {

        ICollection<Tour> GetAll();

        ICollection<Tour> GetByNameContains(string search);

        Tour Add(Tour tour);

        Tour Update(Tour tour);

        void Remove(Tour tour);
    }

    /// <summary>
    /// <see cref="ITourRepository"/> implementation.
    /// </summary>
    public class TourRepository : ITourRepository {

        private readonly PostgreContext _context;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourRepository(PostgreContext context) {
            _context = context;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Get all <see cref="Tour"/>s.
        /// </summary>
        /// <returns>all <see cref="Tour"/>s</returns>
        public ICollection<Tour> GetAll() {
            return _context.Tours.ToList();
        }

        /// <summary>
        /// Get all <see cref="Tour"/>s whose name contains (case insensitive) the passed <paramref name="search"/>.
        /// </summary>
        /// <param name="search">search string</param>
        /// <returns>The found <see cref="Tour"/>s</returns>
        public ICollection<Tour> GetByNameContains(string search) {
            return _context.Tours.Where(t => t.Name.ToLower().Contains(search.ToLower())).ToList();
        }

        // TODO: Comment
        public Tour Add(Tour tour) {
            _context.Add(tour);
            _context.SaveChanges();
            return tour;
        }

        // TODO: Comment
        public Tour Update(Tour tour) {
            _context.Update(tour);
            _context.SaveChanges();
            return tour;
        }

        // TODO: Comment
        public void Remove(Tour tour) {
            _context.Remove(tour);
            _context.SaveChanges();
        }
    }
}
