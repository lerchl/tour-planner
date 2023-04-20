using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    public interface ITourService : IEntityService<Tour> {

        public List<Tour> GetByNameContains(string search);
    }
}