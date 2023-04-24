namespace TourPlanner.Logic.Service.Except {

    /// <summary>
    ///     Exception thrown when the key is invalid.
    /// </summary>
    public class InvalidKeyException : Exception {

        public InvalidKeyException() : base("Invalid MapQuest API key.") {
            // noop
        }
    }
}
