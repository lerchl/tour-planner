using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     Interface for <see cref="Tour"/> repository.
    /// </summary>
    public interface ITourRepository {

        /// <summary>
        ///     Get all <see cref="Tour"/>s.
        /// </summary>
        /// <returns>all <see cref="Tour"/>s</returns>
        ICollection<Tour> GetAll();

        /// <summary>
        ///     Get all <see cref="Tour"/>s whose name contains (case insensitive) the passed <paramref name="search"/>.
        /// </summary>
        /// <param name="search">search string</param>
        /// <returns>The found <see cref="Tour"/>s</returns>
        ICollection<Tour> GetByNameContains(string search);

        /// <summary>
        ///     Add a <see cref="Tour"/> to the database.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/> to add</param>
        /// <returns>The added <see cref="Tour"/></returns>
        Tour Add(Tour tour);

        /// <summary>
        ///     Update a <see cref="Tour"/> in the database.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/> to update</param>
        /// <returns>The updated <see cref="Tour"/></returns>
        Tour Update(Tour tour);

        /// <summary>
        ///     Remove a <see cref="Tour"/> from the database.
        /// </summary>
        /// <param name="tour">the <see cref="Tour"/> to remove</param>
        void Remove(Tour tour);
    }

    /// <summary>
    ///     <see cref="ITourRepository"/> implementation.
    /// </summary>
    public class TourRepository : Singleton<TourRepository>, ITourRepository {

        public ICollection<Tour> GetAll() {
            using var context = new PostgreContext();
            return context.Tours.ToList();
        }

        public ICollection<Tour> GetByNameContains(string search) {
            using var context = new PostgreContext();
            return context.Tours.Where(t => t.Name.ToLower().Contains(search.ToLower())).ToList();
        }

        public Tour Add(Tour tour) {
            using var context = new PostgreContext();
            context.Add(tour);
            context.SaveChanges();
            return tour;
        }

        public Tour Update(Tour tour) {
            using var context = new PostgreContext();
            context.Update(tour);
            context.SaveChanges();
            return tour;
        }

        public void Remove(Tour tour) {
            using var context = new PostgreContext();
            context.Remove(tour);
            context.SaveChanges();
        }
    }
}
