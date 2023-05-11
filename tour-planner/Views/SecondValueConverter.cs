using System;

namespace TourPlanner.Views {

    public class SecondValueConverter : TimeValueConverter {

        public SecondValueConverter() : base(seconds => TimeSpan.FromSeconds(seconds), DAYS, HOURS, MINUTES, SECONDS) {
            // noop
        }
    }
}
