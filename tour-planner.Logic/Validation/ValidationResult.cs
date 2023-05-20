namespace TourPlanner.Logic.Validation {

    public class ValidationResult {

        public List<ValidationMessage> Messages { get; } = new();

        public bool Valid {
            get => !Messages.Where(m => m.Status == ValidationMessageStatus.Error).Any();
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void AddMessage(ValidationMessage message) {
            Messages.Add(message);
        }
    }
}
