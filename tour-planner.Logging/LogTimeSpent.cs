using System.Linq;
using System.Diagnostics;
using Metalama.Framework.Aspects;

namespace TourPlanner.Logging {

    /// <summary>
    ///     Aspect that logs the time a method spent executing.
    /// </summary>
    public class LogTimeSpent : OverrideMethodAspect {

        private readonly bool _logStart;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public LogTimeSpent(bool logStart = false) {
            _logStart = logStart;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public override dynamic? OverrideMethod() {
            // Get logger for the class that called the method
            var logger = LoggerFactory.GetLogger(meta.This.ToString());
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Log when the method starts executing, if requested
            if (_logStart) {
                logger.Debug($"Executing {meta.Target.Method.Name}...");
            }

            // Execute the method
            var result = meta.Proceed();

            stopwatch.Stop();
            // Log the method's name and the time it took to execute
            logger.Debug($"{meta.Target.Method.Name} took {stopwatch.ElapsedMilliseconds}ms");
            return result;
        }
    }
}
