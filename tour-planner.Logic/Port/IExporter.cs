namespace TourPlanner.Logic.Port {

    public interface ISerializer<T> {

        public string Serialize(List<T> list);
    }
}
