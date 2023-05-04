using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourLogDialogViewModel : EntityDialogViewModel<TourLog> {

        private int _hours = 0;
        public int Hours {
            get => _hours;
            set {
                _hours = value;
                PropertyChanged?.Invoke(this, new(nameof(Hours)));
            }
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public TourLogDialogViewModel(TourLogService tourLogService) : base(tourLogService) {
            // noop
        }
    }
}
