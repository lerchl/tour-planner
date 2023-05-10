namespace TourPlanner.Model {

    public class Rating {

        public static HashSet<Rating> ALL {
            get => new() { GARBAGE, BAD, OK, GOOD, GREAT, EXCELLENT };
        }

        public static readonly Rating GARBAGE = new(100, "Garbage", 0);
        public static readonly Rating BAD = new(200, "Bad", 1);
        public static readonly Rating OK = new(300, "Ok", 2);
        public static readonly Rating GOOD = new(400, "Good", 3);
        public static readonly Rating GREAT = new(500, "Great", 4);
        public static readonly Rating EXCELLENT = new(600, "Excellent", 5);

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Value { get; private set; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        private Rating(int id, string name, int value) {
            Id = id;
            Name = name;
            Value = value;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public static Rating FromId(int id) {
            return ALL.FirstOrDefault(t => t!.Id == id, null) ??
                    throw new ArgumentException($"No rating with id {id} found.");
        }
    }
}
