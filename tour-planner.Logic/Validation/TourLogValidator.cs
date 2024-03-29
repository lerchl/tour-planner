using TourPlanner.Model;

using static TourPlanner.Logic.Validation.ValidationUtils;

namespace TourPlanner.Logic.Validation {

    /// <summary>
    ///     <see cref="IValidator{T}"/> implementation for <see cref="TourLog"/>s.
    /// </summary>
    public class TourLogValidator : IValidator<TourLog> {

        public ValidationResult ValidateSave(TourLog t) {
            var result = new ValidationResult();

            // Date
            ValidateRequired(t.DateTime, "Date", result);

            // Time
            ValidateMinValue(t.Duration, 1, "Time", result);

            // Comment
            ValidateMaxLength(t.Comment, 1000, "Comment", result);

            // Tour
            ValidateRequired(t.Tour, "Tour", result);

            return result;
        }
    }
}
