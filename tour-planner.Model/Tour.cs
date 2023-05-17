using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Model {

    [Table("tours")]
    public class Tour : Entity {

        [Column("id"), Key]
        public Guid Id { get; set; }

        [Column("name"), MaxLength(100), Required]
        public string Name { get; set; } = "";

        [Column("description"), MaxLength(1000)]
        public string Description { get; set; } = "";

        [Column("from"), MaxLength(100)]
        public string From { get; set; } = "";

        [Column("to"), MaxLength(100)]
        public string To { get; set; } = "";

        [Column("transport_type")]
        public TransportType TransportType { get; set; } = TransportType.FASTEST;

        [Column("last_edited")]
        public DateTime? LastEdited { get; set; }

        /// <summary>
        ///     The last time the route of this tour has been fetched from the api.
        /// </summary>
        [Column("last_fetched")]
        public DateTime? LastFetched { get; set; }

        /// <summary>
        ///     Distance in meter.
        /// </summary>
        [Column("distance")]
        public double? Distance { get; set; }

        /// <summary>
        ///     Estimated time for the tour in seconds.
        /// </summary>
        [Column("estimated_time")]
        public long? EstimatedTime { get; set; }

        [Column("map_image")]
        public byte[]? MapImage { get; set; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public Tour() {
            // default constructor
        }

        public Tour(Tour other) {
            Id = other.Id;
            Name = other.Name;
            Description = other.Description;
            From = other.From;
            To = other.To;
            TransportType = other.TransportType;
            LastFetched = other.LastFetched;
            Distance = other.Distance;
            EstimatedTime = other.EstimatedTime;
            MapImage = other.MapImage;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public override Guid GetGuid() {
            return Id;
        }
    }
}
