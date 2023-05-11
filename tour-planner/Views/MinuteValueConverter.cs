using System;

namespace TourPlanner.Views {

    public class MinuteValueConverter : TimeValueConverter {

        public MinuteValueConverter() : base(minutes => TimeSpan.FromMinutes(minutes), DAYS, HOURS, MINUTES) {
            // noop
        }
    }
}
