using static TourPlanner.Logic.Validation.ValidationMessageStatus;

namespace TourPlanner.Logic.Validation {

    /// <summary>
    ///     Utility class for validation.
    /// </summary>
    public class ValidationUtils {

        private ValidationUtils() {
            // Utils class should not be instantiated
            throw new NotSupportedException();
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        // strings

        public static void ValidateRequired(object? o, string name, ValidationResult result) {
            if (o == null) {
                result.AddMessage(new(Error, $"{name} is required"));
            }
        }

        public static void ValidateMinLength(string? s, int minLength, string name, ValidationResult result) {
            if (s?.Length < minLength) {
                result.AddMessage(new(Error, $"{name} is too short (min. {minLength} characters)"));
            }
        }

        public static void ValidateMaxLength(string? s, int maxLength, string name, ValidationResult result) {
            if (s?.Length > maxLength) {
                result.AddMessage(new(Error, $"{name} is too long (max. {maxLength} characters)"));
            }
        }

        public static void ValidateRequiredString(string? s, int minLength, int maxLength, string name, ValidationResult result) {
            ValidateRequired(s, name, result);
            ValidateMinLength(s, minLength, name, result);
            ValidateMaxLength(s, maxLength, name, result);
        }

        public static void ValidateUrl(string? url, string name, ValidationResult result) {
            if (url == null) {
                return;
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) {
                result.AddMessage(new(Error, $"{name} is not a valid URL"));
            }
        }

        // numbers

        public static void ValidateMinValue(int value, int minValue, string name, ValidationResult result) {
            if (value < minValue) {
                result.AddMessage(new(Error, $"{name} is too small (min. {minValue})"));
            }
        }

        public static void ValidateMaxValue(int value, int maxValue, string name, ValidationResult result) {
            if (value > maxValue) {
                result.AddMessage(new(Error, $"{name} is too large (max. {maxValue})"));
            }
        }

        public static void ValidateRange(int value, int minValue, int maxValue, string name, ValidationResult result) {
            ValidateMinValue(value, minValue, name, result);
            ValidateMaxValue(value, maxValue, name, result);
        }
    }
}