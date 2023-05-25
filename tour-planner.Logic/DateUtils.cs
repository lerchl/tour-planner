namespace TourPlanner.Logic {

    public static class DateUtils {

        public const string TIME_FORMAT = "HH:mm:ss";
        public const string DATE_FORMAT = "dd.MM.yyyy";
        public const string DATE_TIME_FORMAT = $"{DATE_FORMAT} {TIME_FORMAT}";

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public static string FormatDateTime(DateTime dateTime) {
            return dateTime.ToString(DATE_TIME_FORMAT);
        }

        public static DateTime ParseDateTime(string dateTime) {
            return DateTime.ParseExact(dateTime, DATE_TIME_FORMAT, null);
        }
    }
}
