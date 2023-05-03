using System;
using System.Globalization;
using System.Windows.Data;

namespace TourPlanner.Views {

    public class VisibleIfNullConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null ? "Visible" : "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null!;
        }
    }
}
