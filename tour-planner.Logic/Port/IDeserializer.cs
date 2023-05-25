namespace TourPlanner.Logic.Port {

    public interface IDeserializer<T> {

        public List<T> Deserialize(string str);
    }
}
