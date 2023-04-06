namespace TourPlanner.Logic.Validation {
    public class ValidationException : Exception {

        public ValidationResult Result { get; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public ValidationException(ValidationResult result) {
            Result = result;
        }
    }
}
