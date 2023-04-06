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
        public int Time { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [ForeignKey("tour_id"), Required]
        public Tour? Tour { get; set; }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public override Guid GetGuid() {
            return Id;
        }
    }
}
