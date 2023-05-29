using System.Text;

namespace TourPlanner.Logic {

    /// <summary>
    ///     Converts a time value, stored in a single unit, e.g. seconds or minutes, to a human readable format.
    /// </summary>
    public class TimeConverter {

        public static readonly Tuple<Func<TimeSpan, int>, string> DAYS = new(timeSpan => timeSpan.Days, "d");
        public static readonly Tuple<Func<TimeSpan, int>, string> HOURS = new(timeSpan => timeSpan.Hours, "h");
        public static readonly Tuple<Func<TimeSpan, int>, string> MINUTES = new(timeSpan => timeSpan.Minutes, "m");
        public static readonly Tuple<Func<TimeSpan, int>, string> SECONDS = new(timeSpan => timeSpan.Seconds, "s");

        private readonly Func<long, TimeSpan> _timeSpanMapper;
        private readonly Tuple<Func<TimeSpan, int>, string>[] _unitMappers;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TimeConverter(Func<long, TimeSpan> timeSpanMapper, params Tuple<Func<TimeSpan, int>, string>[] unitMappers) {
            _timeSpanMapper = timeSpanMapper;
            _unitMappers = unitMappers;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Convert a time value to a human readable format.
        /// </summary>
        /// <param name="time">the time value</param>
        /// <returns>the human readable format</returns>
        public string Convert(long time) {
            var timeSpan = _timeSpanMapper(time);
            var stringBuilder = new StringBuilder();

            foreach ((Func<TimeSpan, int> mapper, string unit) in _unitMappers) {
                int unitValue = mapper(timeSpan);

                if (unitValue > 0) {
                    stringBuilder.Append($"{unitValue}{unit} ");
                }
            }

            return stringBuilder.ToString().Trim();
        }
    }
}
