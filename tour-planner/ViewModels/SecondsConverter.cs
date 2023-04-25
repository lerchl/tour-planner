using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;

namespace TourPlanner.ViewModels {

    /// <summary>
    ///     Converts an <see cref="long"/> representing seconds to a user-readable <see cref="string"/>.
    /// </summary>
    public class SecondsConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return null!;
            }

            try {
                long seconds = (long) value;
                var timeSpan = TimeSpan.FromSeconds(seconds);
                List<Tuple<int, string>> time = new() {
                    new(timeSpan.Days, "d"),
                    new(timeSpan.Hours, "h"),
                    new(timeSpan.Minutes, "m")
                };

                var stringBuilder = new StringBuilder();
                time.ForEach(t => {
                    if (t.Item1 > 0) {
                        stringBuilder.Append($"{t.Item1}{t.Item2}");
                        if (time.IndexOf(t) < time.Count - 1) {
                            stringBuilder.Append(' ');
                        }
                    }
                });

                return stringBuilder.ToString();
            } catch (InvalidCastException e) {
                throw new ArgumentException("Value must be of type long.", e);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return 0;
        }
    }
}
