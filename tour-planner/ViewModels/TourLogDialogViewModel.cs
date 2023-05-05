using System.Collections.Generic;
using System.Linq;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourLogDialogViewModel : EntityDialogViewModel<TourLog> {

        private int _days = 0;
        public int Days {
            get => _days;
            set {
                _days = value;
                PropertyChanged?.Invoke(this, new(nameof(Days)));
            }
        }

        private int _hours = 0;
        public int Hours {
            get => _hours;
            set {
                _hours = value;
                PropertyChanged?.Invoke(this, new(nameof(Hours)));
            }
        }

        private int _minutes = 0;
        public int Minutes {
            get => _minutes;
            set {
                _minutes = value;
                PropertyChanged?.Invoke(this, new(nameof(Minutes)));
            }
        }

        public List<int> Ratings => Enumerable.Range(1, 10).ToList();

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public TourLogDialogViewModel(ITourLogService tourLogService) : base(tourLogService) {
            // noop
        }
    }
}
