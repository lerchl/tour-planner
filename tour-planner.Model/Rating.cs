namespace TourPlanner.Model {

    public class Rating : EnumLike<Rating, int> {

        public static HashSet<Rating> ALL {
            get => new() { GARBAGE, BAD, OK, GOOD, GREAT, EXCELLENT };
        }

        public static readonly Rating GARBAGE = new(100, "Garbage", 0);
        public static readonly Rating BAD = new(200, "Bad", 1);
        public static readonly Rating OK = new(300, "Ok", 2);
        public static readonly Rating GOOD = new(400, "Good", 3);
        public static readonly Rating GREAT = new(500, "Great", 4);
        public static readonly Rating EXCELLENT = new(600, "Excellent", 5);

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        protected Rating(int id, string name, int value) : base(id, name, value) {
            // noop
        }
    }
}
