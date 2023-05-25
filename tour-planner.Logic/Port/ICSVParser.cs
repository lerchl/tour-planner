namespace TourPlanner.Logic.Port {

    public interface ICSVParser<T> : ISerializer<T>, IDeserializer<T> {
        // noop
    }
}
