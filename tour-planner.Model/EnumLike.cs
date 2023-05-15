namespace TourPlanner.Model {

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

        public static I FromId(int id, IEnumerable<I> all) {
            return all.FirstOrDefault(t => t!.Id == id, null) ??
                    throw new ArgumentException($"No transport type with id {id} found.");
        }
    }
}
