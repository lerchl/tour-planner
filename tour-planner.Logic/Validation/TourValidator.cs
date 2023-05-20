using TourPlanner.Model;

using static TourPlanner.Logic.Validation.ValidationUtils;

namespace TourPlanner.Logic.Validation {

    /// <summary>
    ///     <see cref="IValidator{T}"/> implementation for <see cref="Tour"/>s.
    /// </summary>
    public class TourValidator : IValidator<Tour> {

        public ValidationResult ValidateSave(Tour t) {
            var result = new ValidationResult();

            // Name
            ValidateRequiredString(t.Name, 2, 100, "Name", result);

            // Description
            ValidateMaxLength(t.Description, 1000, "Description", result);

            // From
            ValidateRequiredString(t.From, 2, 100, "From", result);

            // To
            ValidateRequiredString(t.To, 2, 100, "To", result);

            return result;
        }
    }
}
