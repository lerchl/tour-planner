using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.Logic.Search {

    /// <summary>
    ///     Matcher for the childfriendliness of a <see cref="Tour"/>.
    /// </summary>
    public class ChildfriendlinessMatcher : SingleValueMatcher<double> {

        public ChildfriendlinessMatcher(ITourService tourService) : base(tourService) {
            // noop
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected override double GetValue(Tour tour) {
            return _tourService.GetChildFriendliness(tour);
        }
    }
}
