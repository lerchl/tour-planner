using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    public interface ITourService : ICrudService<Tour> {

        public List<Tour> GetByNameContains(string search);

        public int GetPopularityRank(Tour tour);
    }
}
