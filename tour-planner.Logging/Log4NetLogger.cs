namespace TourPlanner.Logging {

    public class Log4NetLogger : ILogger {

        private readonly log4net.ILog _log;

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public Log4NetLogger(string configPath, string caller) {
            if (!File.Exists(configPath)) {
                throw new ArgumentException("Does not exist.", nameof(configPath));
            }

            log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
            _log = log4net.LogManager.GetLogger(caller);
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void Info(string message) {
            _log.Info(message);
        }

        public void Warn(string message) {
            _log.Warn(message);
        }

        public void Error(string message) {
            _log.Error(message);
        }

        public void Fatal(string message) {
            _log.Fatal(message);
        }

        public void Debug(string message) {
            _log.Debug(message);
        }
    }
}
