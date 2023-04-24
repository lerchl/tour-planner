namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     Exception thrown when the locations are invalid.
    /// </summary>
    public class InvalidLocationsException : Exception {

        public string From { get; private set; }
        public string To { get; private set; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public InvalidLocationsException(string from, string to) : base($"Unknown locations from {from} to {to}.") {
            From = from;
            To = to;
        }
    }
}
