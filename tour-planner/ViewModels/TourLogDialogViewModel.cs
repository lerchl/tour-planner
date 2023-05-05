using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourLogDialogViewModel : EntityDialogViewModel<TourLog> {

        private const int MINUTES_PER_DAY = MINUTES_PER_HOUR * 24;
        private const int MINUTES_PER_HOUR = 60;

        private int _days = 0;
        public int Days {
            get => _days;
            set {
                _days = value;
                RaisePropertyChanged();
            }
        }

        private int _hours = 0;
        public int Hours {
            get => _hours;
            set {
                _hours = value;
                RaisePropertyChanged();
            }
        }

        private int _minutes = 0;
        public int Minutes {
            get => _minutes;
            set {
                _minutes = value;
                RaisePropertyChanged();
            }
        }

        [SuppressMessage("Usage", "CA1822", Justification = "Intended for use in XAML.")]
        public List<int> Ratings => Enumerable.Range(1, 10).ToList();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourLogDialogViewModel(ITourLogService tourLogService) : base(tourLogService) {
            // noop
        }

        public override void Init(TourLog entity, Action close, Action<bool> setDialogResult) {
            // Set days
            Days = entity.Time / MINUTES_PER_DAY;
            // Calculate remaining hours and minutes
            int hoursInMinutes = entity.Time - MINUTES_PER_DAY;
            // Set hours
            Hours = hoursInMinutes >= 0 ? hoursInMinutes / MINUTES_PER_HOUR : 0;
            // Set minutes
            Minutes = entity.Time % MINUTES_PER_HOUR;
            base.Init(entity, close, setDialogResult);
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected override void Save() {
            Entity.Time = MINUTES_PER_DAY * Days + MINUTES_PER_HOUR * Hours + Minutes;
            base.Save(); 
        }
    }
}
