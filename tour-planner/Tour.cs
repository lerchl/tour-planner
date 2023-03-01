namespace tour_planner {

    internal class Tour {

        public string Name { get; set; }
        public double Distance { get; set; }
        public double Elevation { get; set; }
        public double Difficulty { get; set; }

        // /////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////
    
        public Tour(string name, double distance, double elevation, double difficulty) {
            Name = name;
            Distance = distance;
            Elevation = elevation;
            Difficulty = difficulty;
        }
    }
}
