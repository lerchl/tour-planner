namespace TourPlanner.Model {

    public class Difficulty {

        public static HashSet<Difficulty> ALL {
            get => new() { EASY, MEDIUM, HARD };
        }

        public static readonly Difficulty EASY = new(100, "Easy", 0);
        public static readonly Difficulty MEDIUM = new(200, "Medium", 1);
        public static readonly Difficulty HARD = new(300, "Hard", 2);

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Value { get; private set; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        private Difficulty(int id, string name, int value) {
            Id = id;
            Name = name;
            Value = value;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public static Difficulty FromId(int id) {
            return ALL.FirstOrDefault(r => r!.Id == id, null) ??
                    throw new ArgumentException($"No difficulty with id {id} found.");
        }
    }
}
