using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.ViewModels {

    public abstract class BaseViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
