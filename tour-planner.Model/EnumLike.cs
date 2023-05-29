namespace TourPlanner.Model {

    /// <summary>
    ///     Base class for all enum-like classes.
    /// </summary>
    public class EnumLike<I, V> where I : EnumLike<I, V> {

        public int Id { get; private set; }
        public string Name { get; private set; }
        public V Value { get; private set; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        protected EnumLike(int id, string name, V value) {
            Id = id;
            Name = name;
            Value = value;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Tries to find an <see cref="EnumLike{I, V}"/> with the given <paramref name="id"/> in the given <paramref name="all"/>.
        /// </summary>
        /// <param name="id">The id of the <see cref="EnumLike{I, V}"/> to find</param>
        /// <param name="all">The <see cref="IEnumerable{I}"/> to search in</param>
        /// <returns>The <see cref="EnumLike{I, V}"/> with the given <paramref name="id"/>.</returns>
        public static I FromId(int id, IEnumerable<I> all) {
            return all.FirstOrDefault(t => t!.Id == id, null) ??
                    throw new ArgumentException($"No {typeof(I).FullName} with id {id} found.");
        }

        /// <summary>
        ///     Tries to find an <see cref="EnumLike{I, V}"/> with the given <paramref name="value"/> in the given <paramref name="all"/>.
        /// </summary>
        /// <param name="value">The value of the <see cref="EnumLike{I, V}"/> to find</param>
        /// <param name="all">The <see cref="IEnumerable{I}"/> to search in</param>
        /// <returns>The <see cref="EnumLike{I, V}"/> with the given <paramref name="value"/>.</returns>
        public static I FromValue(V value, IEnumerable<I> all) {
            return all.FirstOrDefault(t => t!.Value?.Equals(value) == true, null) ??
                    throw new ArgumentException($"No {typeof(I).FullName} with value {value} found.");
        }
    }
}
