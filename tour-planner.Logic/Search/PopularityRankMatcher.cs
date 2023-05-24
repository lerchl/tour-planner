using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.Logic.Search {

    public class PopularityRankMatcher : SingleValueMatcher<int> {

        public PopularityRankMatcher(ITourService tourService) : base(tourService) {
            // noop
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected override int GetValue(Tour tour) {
            return _tourService.GetPopularityRank(tour);
        }
    }
}
