namespace TourPlanner.Logic.Port {

    /// <summary>
    ///     Interface that defines the methods for serializing a list of objects into a string.
    /// </summary>
    public interface ISerializer<T> {

        /// <summary>
        ///     Serialize a list of objects into a string.
        /// </summary>
        /// <param name="list">The list of objects to serialize</param>
        /// <returns>The serialized string.</returns>
        public string Serialize(List<T> list);
    }
}
