namespace TourPlanner.Data {

    /// <summary>
    ///     Singleton base class.
    /// </summary>
    abstract public class Singleton<T> where T : class, new() {

        private static T? _instance;

        public static T Instance {
            get {
                _instance ??= new T();
                return _instance;
            }
            private set => _instance = value;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        protected Singleton() {
            // noop
        }
    }
}
