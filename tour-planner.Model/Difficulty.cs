namespace TourPlanner.Model {

    public class Difficulty : EnumLike<Difficulty, int> {

        public static HashSet<Difficulty> ALL {
            get => new() { EASY, MEDIUM, HARD };
        }

        public static readonly Difficulty EASY = new(100, "Easy", 0);
        public static readonly Difficulty MEDIUM = new(200, "Medium", 1);
        public static readonly Difficulty HARD = new(300, "Hard", 2);

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        protected Difficulty(int id, string name, int value) : base(id, name, value) {
            // noop
        }
    }
}
