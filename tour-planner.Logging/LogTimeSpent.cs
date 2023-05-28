using System.Diagnostics;
using Metalama.Framework.Aspects;

namespace TourPlanner.Logging {

    /// <summary>
    ///     Aspect that logs the time a method spent executing.
    /// </summary>
    public class LogTimeSpent : OverrideMethodAspect {

        public override dynamic? OverrideMethod() {
            // Get logger for the class that called the method
            var logger = LoggerFactory.GetLogger(meta.This.ToString());
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Execute the method
            var result = meta.Proceed();

            stopwatch.Stop();
            // Log the method's name and the time it took to execute
            logger.Debug($"{meta.Target.Method.Name} took {stopwatch.ElapsedMilliseconds}ms");
            return result;
        }
    }
}
