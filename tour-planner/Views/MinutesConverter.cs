using System;

namespace TourPlanner.Views {

    public class MinutesConverter : TimeConverter {

        public MinutesConverter() : base(minutes => TimeSpan.FromMinutes(minutes), DAYS, HOURS, MINUTES) {
            // noop
        }
    }
}
