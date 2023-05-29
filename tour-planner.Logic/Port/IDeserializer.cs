namespace TourPlanner.Logic.Port {

    /// <summary>
    ///     Interface that defines the methods for deserializing a string into a list of objects.
    /// </summary>
    public interface IDeserializer<T> {

        /// <summary>
        ///     Deserialize a string into a list of objects.
        /// </summary>
        /// <param name="str">The string to deserialize</param>
        /// <returns>The deserialized list of objects.</returns>
        public List<T> Deserialize(string str);
    }
}
