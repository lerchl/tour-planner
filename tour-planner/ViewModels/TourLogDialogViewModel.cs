using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public class TourLogDialogViewModel : EntityDialogViewModel<TourLog> {

        private const long MINUTES_PER_DAY = MINUTES_PER_HOUR * 24;
        private const long MINUTES_PER_HOUR = 60;

        private DateTime _date = DateTime.Now;
        public DateTime Date {
            get => _date;
            set {
                _date = value;
                RaisePropertyChanged();
            }
        }

        private long _days = 0;
        public long Days {
            get => _days;
            set {
                _days = value;
                RaisePropertyChanged();
            }
        }

        private long _hours = 0;
        public long Hours {
            get => _hours;
            set {
                _hours = value;
                RaisePropertyChanged();
            }
        }

        private long _minutes = 0;
        public long Minutes {
            get => _minutes;
            set {
                _minutes = value;
                RaisePropertyChanged();
            }
        }

        [SuppressMessage("Usage", "CA1822", Justification = "Intended for use in XAML.")]
        public List<Rating> Ratings => Rating.ALL.ToList();

        [SuppressMessage("Usage", "CA1822", Justification = "Intended for use in XAML.")]
        public List<Difficulty> Difficulties => Difficulty.ALL.ToList();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourLogDialogViewModel(ITourLogService tourLogService) : base(tourLogService) {
            // noop
        }

        public override void Init(TourLog entity, Action close, Action<bool> setDialogResult) {
            // Set days
            Days = entity.Duration / MINUTES_PER_DAY;
            // Calculate remaining hours and minutes
            long hoursInMinutes = entity.Duration - (Days * MINUTES_PER_DAY);
            // Set hours
            Hours = hoursInMinutes >= 0 ? hoursInMinutes / MINUTES_PER_HOUR : 0;
            // Set minutes
            Minutes = entity.Duration % MINUTES_PER_HOUR;
            base.Init(entity, close, setDialogResult);
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected override void Save() {
            Entity.Date = Date;
            Entity.Duration = MINUTES_PER_DAY * Days + MINUTES_PER_HOUR * Hours + Minutes;
            base.Save();
        }
    }
}
