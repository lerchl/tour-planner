using System.Collections.ObjectModel;
using System.ComponentModel;

namespace tour_planner {

    internal class MainViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
    
        public ObservableCollection<Tour> Tours { get; } = new ObservableCollection<Tour>() {
            new Tour("Tour 1", 1000, 200, 2.5),
            new Tour("Tour 2", 2000, 400, 3.5),
            new Tour("Tour 3", 3000, 600, 4.5)
        };
    }
}