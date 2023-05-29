using TourPlanner.Model;

namespace TourPlanner.Logic.Port {

    /// <summary>
    ///     Interface that defines the methods for serializing and deserializing <see cref="TourLog"/>s to and from CSV.
    /// </summary>
    public interface ITourLogCSVParser : ICSVParser<TourLog> {
        // noop
    }
}
