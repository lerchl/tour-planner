using TourPlanner.Model;

using static TourPlanner.Logic.DateUtils;

namespace TourPlanner.Logic.Search {

    public static class ComparisonUtils {

        public static bool ContainsIgnoreCase(object? str, string search) {
            if (str == null) {
                return false;
            }

            return str.ToString()?.ToLower().Contains(search.ToLower()) ?? false;
        }

        public static bool ContainsIgnoreCase<E, V>(EnumLike<E, V>? enumLike, string search) where E : EnumLike<E, V> {
            return ContainsIgnoreCase(enumLike?.Name, search);
        }

        public static bool ContainsIgnoreCase(DateTime? dateTime, string search) {
            if (dateTime == null) {
                return false;
            }

            return ContainsIgnoreCase(FormatDateTime(dateTime.Value, DATE_TIME_FORMAT_WITH_SECONDS), search);
        }
    }
}
