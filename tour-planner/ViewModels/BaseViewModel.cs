using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TourPlanner.ViewModels {

    public abstract class BaseViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected static void InUI(Action action) {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
