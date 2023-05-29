using System;
using System.Threading;
using TourPlanner.Logging;

namespace TourPlanner.ViewModels {

    public class TourListFilterRequest {

        private static readonly ILogger _logger = LoggerFactory.GetLogger<TourListFilterRequest>();
        private static long _lastId = 0;

        private readonly long _id;
        private readonly Action _filter;
        
        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourListFilterRequest(Action filter) {
            _id = ++_lastId;
            _filter = filter;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void Filter() {
            _logger.Debug("Queued filter request");

            if (_id < _lastId) {
                _logger.Debug("Filter request is outdated");
                return;
            }

            Thread.Sleep(400);

            if (_id < _lastId) {
                _logger.Debug("Filter request is outdated");
                return;
            }

            _logger.Debug("Filtering...");
            _filter();
        }
    }
}
