using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Model {

    [Table("tours")]
    public class Tour : Entity {

        [Column("id"), Key]
        public Guid Id { get; set; }

        [Column("name"), MaxLength(100), Required]
        public string? Name { get; set; }

        [Column("description"), MaxLength(1000)]
        public string? Description { get; set; }

        [Column("from"), MaxLength(100), Required]
        public string? From { get; set; }

        [Column("to"), MaxLength(100), Required]
        public string? To { get; set; }

        [Column("transport_type"), MaxLength(100)]
        public string? TransportType { get; set; }

        /// <summary>
        ///     Distance in meter.
        /// </summary>
        [Column("distance")]
        public int Distance { get; set; }

        /// <summary>
        ///     Estimated time for the tour in minutes.
        /// </summary>
        [Column("estimated_time")]
        public int EstimatedTime { get; set; }

        [Column("image_url"), MaxLength(1000)]
        public string? ImageUrl { get; set; }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public override Guid GetGuid() {
            return Id;
        }
    }
}
