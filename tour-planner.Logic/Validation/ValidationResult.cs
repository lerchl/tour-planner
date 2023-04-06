namespace TourPlanner.Logic.Validation {

    public class ValidationResult {

        public List<ValidationMessage> Messages { get; } = new();

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void AddMessage(ValidationMessage message) {
            Messages.Add(message);
        }

        public bool IsValid() {
            return !Messages.Where(m => m.Status == ValidationMessageStatus.Error).Any();
        }
    }
}
