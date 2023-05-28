namespace TourPlanner.Logic {

    public static class DateUtils {

        public const string TIME_FORMAT_WITH_SECONDS = "HH:mm:ss";
        public const string TIME_FORMAT_WITHOUT_SECONDS = "HH:mm";
        public const string DATE_FORMAT = "dd.MM.yyyy";
        public const string DATE_TIME_FORMAT_WITH_SECONDS = $"{DATE_FORMAT} {TIME_FORMAT_WITH_SECONDS}";
        public const string DATE_TIME_FORMAT_WITHOUT_SECONDS = $"{DATE_FORMAT} {TIME_FORMAT_WITHOUT_SECONDS}";

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public static string FormatDateTime(DateTime dateTime, string format) {
            return dateTime.ToString(format);
        }

        public static DateTime ParseDateTime(string dateTime, string format) {
            return DateTime.ParseExact(dateTime, format, null);
        }
    }
}
