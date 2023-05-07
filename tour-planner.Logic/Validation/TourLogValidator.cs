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
            ValidateRequired(t.Date, "Date", result);

            // Time
            ValidateMinValue(t.Time, 1, "Time", result);

            // Rating
            ValidateRange(t.Rating, 1, 10, "Rating", result);

            // Tour
            ValidateRequired(t.Tour, "Tour", result);

            return result;
        }
    }
}
