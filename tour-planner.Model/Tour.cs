namespace TourPlanner.Model {

    public class Tour {

        // from the database
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? TransportType { get; set; }

        // from the api
        public double? Distance { get; set; }
        public double? EstimatedTime { get; set; }
        public string? ImageUrl { get; set; }

        // /////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////
    
        public Tour(string name) {
            Name = name;
        }
    }
}
