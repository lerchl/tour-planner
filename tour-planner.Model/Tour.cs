using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Model {

    [Table("Tours")]
    public class Tour {

        [Key]
        public Guid Id { get; set; }

        [MaxLength(100), Required]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(100), Required]
        public string? From { get; set; }

        [MaxLength(100), Required]
        public string? To { get; set; }

        [MaxLength(100)]
        public string? TransportType { get; set; }

        /// <summary>
        ///     Distance in meter.
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        ///     Estimated time for the tour in minutes.
        /// </summary>
        public int EstimatedTime { get; set; }

        [MaxLength(1000)]
        public string? ImageUrl { get; set; }

        // /////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////

        public Tour(string name) {
            Name = name;
        }
    }
}
