using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace TourPlanner.Views {

    public abstract class TimeConverter : IValueConverter {

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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return null!;
            }

            try {
                var timeSpan = _timeSpanMapper((long) value);
                var stringBuilder = new StringBuilder();

                foreach ((Func<TimeSpan, int> mapper, string unit) in _unitMappers) {
                    int unitValue = mapper(timeSpan);

                    if (unitValue > 0) {
                        stringBuilder.Append($"{unitValue}{unit}");

                        // add space after unit if not last
                        if (unit != _unitMappers[^1].Item2) {
                            stringBuilder.Append(' ');
                        }
                    }
                }

                return stringBuilder.ToString();
            } catch (InvalidCastException e) {
                throw new ArgumentException("Value must be of type long.", e);
            }
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
