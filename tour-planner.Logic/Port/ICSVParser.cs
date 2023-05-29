namespace TourPlanner.Logic.Port {

    /// <summary>
    ///     Interface that defines the methods for serializing and deserializing to and from CSV.
    /// </summary>
    public interface ICSVParser<T> : ISerializer<T>, IDeserializer<T> {
        // noop
    }
}
