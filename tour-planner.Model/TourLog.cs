using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Model {

    [Table("TourLogs")]
    public class TourLog : Entity {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Time in minutes.
        /// </summary>
        public int Time { get; set; }

        public int Rating { get; set; }

        public Tour? Tour { get; set; }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public override Guid GetGuid() {
            return Id;
        }
    }
}
