namespace TourPlanner.Model {

    public class TransportType : EnumLike<TransportType, string> {

        public static HashSet<TransportType> ALL {
            get => new() { FASTEST, SHORTEST, BIKE, FOOT };
        }

        public static readonly TransportType FASTEST = new(0, "Car - Fastest", "fastest");
        public static readonly TransportType SHORTEST = new(1, "Car - Shortest", "shortest");
        public static readonly TransportType BIKE = new(2, "Bike", "bicycle");
        public static readonly TransportType FOOT = new(3, "Foot", "pedestrian");

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected TransportType(int id, string name, string value) : base(id, name, value) {
            // noop
        }
    }
}
