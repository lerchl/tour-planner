using System;

namespace TourPlanner.Views {

    public class SecondsConverter : TimeConverter {

        public SecondsConverter() : base(seconds => TimeSpan.FromSeconds(seconds), DAYS, HOURS, MINUTES, SECONDS) {
            // noop
        }
    }
}
