using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Model {

    [Table("tour_logs")]
    public class TourLog : Entity {

        [Column("id"), Key]
        public Guid Id { get; set; }

        [Column("date"), Required]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Time in minutes.
        /// </summary>
        [Column("time")]
        public long Time { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [ForeignKey("tour_id"), Required]
        public Tour? Tour { get; set; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourLog() {
            // default constructor
        }

        public TourLog(Tour tour) {
            Tour = tour;
        }

        public TourLog(Guid id, DateTime date, int time, int rating, Tour? tour) {
            Id = id;
            Date = date;
            Time = time;
            Rating = rating;
            Tour = tour;
        }



        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public override Guid GetGuid() {
            return Id;
        }
    }
}
