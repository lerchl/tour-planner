namespace TourPlanner.Model {

    public class TransportType {

        public static readonly TransportType FASTEST = new(0, "Fastest", "fastest");
        public static readonly TransportType SHORTEST = new(1, "Shortest", "shortest");
        public static readonly TransportType BIKE = new(2, "Bike", "bicycle");
        public static readonly TransportType FOOT = new(3, "Foot", "pedestrian");

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        private TransportType(int id, string name, string value) {
            Id = id;
            Name = name;
            Value = value;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public static HashSet<TransportType> All() {
            return new HashSet<TransportType> { FASTEST, SHORTEST, BIKE, FOOT };
        }

        public static TransportType FromId(int id) {
            var transportType = All().FirstOrDefault(t => t!.Id == id, null);

            if (transportType == null) {
                throw new ArgumentException($"No transport type with id {id} found.");
            }

            return transportType;
        }
    }
}
