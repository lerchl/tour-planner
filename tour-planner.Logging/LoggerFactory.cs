namespace TourPlanner.Logging {

    public static class LoggerFactory {

        public static ILogger GetLogger<T>() {
            return new Log4NetLogger("log4net.config", typeof(T).FullName!);
        }

        public static ILogger GetLogger(string name) {
            return new Log4NetLogger("log4net.config", name);
        }
    }
}
