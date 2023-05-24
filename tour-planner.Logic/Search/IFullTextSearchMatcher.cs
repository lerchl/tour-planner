using TourPlanner.Model;

namespace TourPlanner.Logic.Search {

    public interface IFullTextSearchMatcher {

        public bool Matches(Tour tour, string search);
    }
}