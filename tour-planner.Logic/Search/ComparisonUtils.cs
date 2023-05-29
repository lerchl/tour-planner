using TourPlanner.Model;

using static TourPlanner.Logic.DateUtils;

namespace TourPlanner.Logic.Search {

    /// <summary>
    ///     Utility methods for comparisons.
    /// </summary>
    public static class ComparisonUtils {

        /// <summary>
        ///     Check if an <see cref="object"/> contains the given <paramref name="search"/> string.
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <param name="search">the string to search for</param>
        /// <returns><c>true</c> if the <paramref name="str"/> contains the <paramref name="search"/> string</returns>
        public static bool ContainsIgnoreCase(object? str, string search) {
            if (str == null) {
                return false;
            }

            return str.ToString()?.ToLower().Contains(search.ToLower()) ?? false;
        }

        /// <summary>
        ///     Check if an <see cref="EnumLike{I, V}"/> contains the given <paramref name="search"/> string.
        ///     See also: <seealso cref="ContainsIgnoreCase(object?, string)"/>
        /// </summary>
        /// <param name="enumLike">the enum-like to check</param>
        /// <param name="search">the string to search for</param>
        /// <typeparam name="I">the <see cref="EnumLike{I, V}"/> type</typeparam>
        /// <typeparam name="V">the <see cref="EnumLike{I, V}"/>'s value type</typeparam>
        /// <returns><c>true</c> if the <paramref name="enumLike"/> contains the <paramref name="search"/> string</returns>
        public static bool ContainsIgnoreCase<I, V>(EnumLike<I, V>? enumLike, string search) where I : EnumLike<I, V> {
            return ContainsIgnoreCase(enumLike?.Name, search);
        }

        /// <summary>
        ///     Check if a <see cref="DateTime"/> contains the given <paramref name="search"/> string.
        ///     See also: <seealso cref="ContainsIgnoreCase(object?, string)"/>
        /// </summary>
        /// <param name="dateTime">the <see cref="DateTime"/> to check</param>
        /// <param name="search">the string to search for</param>
        /// <returns><c>true</c> if the <paramref name="dateTime"/> contains the <paramref name="search"/> string</returns>
        public static bool ContainsIgnoreCase(DateTime? dateTime, string search) {
            if (dateTime == null) {
                return false;
            }

            return ContainsIgnoreCase(FormatDateTime(dateTime.Value, DATE_TIME_FORMAT_WITH_SECONDS), search);
        }
    }
}
