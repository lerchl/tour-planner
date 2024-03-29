using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Model {

    /// <summary>
    ///    Represents a tour log of a <see cref="Tour"/>.
    /// </summary>
    [Table("tour_logs")]
    public class TourLog : Entity {

        [Column("id"), Key]
        public Guid Id { get; set; }

        [Column("date"), Required]
        public DateTime DateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     Duration in minutes.
        /// </summary>
        [Column("duration")]
        public long Duration { get; set; }

        [Column("rating")]
        public Rating Rating { get; set; } = Rating.OK;

        [Column("difficulty")]
        public Difficulty Difficulty { get; set; } = Difficulty.MEDIUM;

        [Column("comment"), MaxLength(1000)]
        public string Comment { get; set; } = "";

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

        public TourLog(TourLog other) {
            Id = other.Id;
            DateTime = other.DateTime;
            Duration = other.Duration;
            Rating = other.Rating;
            Difficulty = other.Difficulty;
            Comment = other.Comment;
            Tour = other.Tour;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public override Guid GetGuid() {
            return Id;
        }
    }
}
