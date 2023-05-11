using System;
using System.Globalization;
using System.Windows.Data;
using TourPlanner.Logic;

namespace TourPlanner.Views {

    /// <summary>
    ///     <see cref="IValueConverter"/> implementation of <see cref="TimeConverter"/>.
    /// </summary>
    public abstract class TimeValueConverter : TimeConverter, IValueConverter {

        protected TimeValueConverter(Func<long, TimeSpan> timeSpanMapper,
                                     params Tuple<Func<TimeSpan, int>, string>[] unitMappers)
                : base(timeSpanMapper, unitMappers) {
            // noop
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return null!;
            }

            try {
                return Convert((long) value);
            } catch (InvalidCastException e) {
                throw new ArgumentException("Value must be of type long.", e);
            }
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
