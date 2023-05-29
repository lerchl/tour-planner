namespace TourPlanner.Model {

    /// <summary>
    ///     Represents the rating of a <see cref="TourLog"/>.
    /// </summary>
    public class Rating : EnumLike<Rating, int> {

        public static HashSet<Rating> ALL {
            get => new() { GARBAGE, BAD, OK, GOOD, GREAT, EXCELLENT };
        }

        public static readonly Rating GARBAGE = new(100, "Garbage", 1);
        public static readonly Rating BAD = new(200, "Bad", 2);
        public static readonly Rating OK = new(300, "Ok", 3);
        public static readonly Rating GOOD = new(400, "Good", 4);
        public static readonly Rating GREAT = new(500, "Great", 5);
        public static readonly Rating EXCELLENT = new(600, "Excellent", 6);

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        protected Rating(int id, string name, int value) : base(id, name, value) {
            // noop
        }
    }
}
