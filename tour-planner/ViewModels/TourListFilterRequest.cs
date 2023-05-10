using System;
using System.Threading;

namespace TourPlanner.ViewModels {

    public class TourListFilterRequest {

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
            if (_id < _lastId) {
                return;
            }

            Thread.Sleep(400);

            if (_id < _lastId) {
                return;
            }

            _filter();
        }
    }
}
