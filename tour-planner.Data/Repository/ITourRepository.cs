using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     <see cref="ICrudRepository{E}"/> for <see cref="Tour"/>s.
    /// </summary>
    public interface ITourRepository : ICrudRepository<Tour> {
        // noop
    }
}
