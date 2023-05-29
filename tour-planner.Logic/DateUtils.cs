namespace TourPlanner.Logic {

    /// <summary>
    ///     Utility class for date and time operations.
    /// </summary>
    public static class DateUtils {

        public const string TIME_FORMAT_WITH_SECONDS = "HH:mm:ss";
        public const string TIME_FORMAT_WITHOUT_SECONDS = "HH:mm";
        public const string DATE_FORMAT = "dd.MM.yyyy";
        public const string DATE_TIME_FORMAT_WITH_SECONDS = $"{DATE_FORMAT} {TIME_FORMAT_WITH_SECONDS}";
        public const string DATE_TIME_FORMAT_WITHOUT_SECONDS = $"{DATE_FORMAT} {TIME_FORMAT_WITHOUT_SECONDS}";

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Format a date time to a string.
        /// </summary>
        /// <param name="dateTime">the date time</param>
        /// <param name="format">the format</param>
        /// <returns>the formatted date time</returns>
        public static string FormatDateTime(DateTime dateTime, string format) {
            return dateTime.ToString(format);
        }

        /// <summary>
        ///     Parse a date time from a string.
        /// </summary>
        /// <param name="dateTime">the date time</param>
        /// <param name="format">the format</param>
        /// <returns>the parsed date time</returns>
        public static DateTime ParseDateTime(string dateTime, string format) {
            return DateTime.ParseExact(dateTime, format, null);
        }
    }
}
