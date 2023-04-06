namespace TourPlanner.Logic.Validation {

    public interface IValidator<T> {

        ValidationResult ValidateSave(T t);
    }
}
