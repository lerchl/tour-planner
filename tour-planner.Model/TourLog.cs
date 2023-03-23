namespace TourPlanner.Model {

    public class TourLog {

        public Tour Tour { get; set; }
        public DateTime Date { get; set; }
        public string Difficulty { get; set; }
        public double Time { get; set; }
        public int Rating { get; set; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////
    
        public TourLog(Tour tour, DateTime date, string difficulty, double time, int rating) {
            Tour = tour;
            Date = date;
            Difficulty = difficulty;
            Time = time;
            Rating = rating;
        }
    }
}
