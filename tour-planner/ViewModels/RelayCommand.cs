using System;
using System.Windows.Input;

namespace TourPlanner.ViewModels {

    public class RelayCommand : ICommand {

        public event EventHandler? CanExecuteChanged;

        private readonly Action<object?> _execute;
        private readonly Predicate<object?> _canExecute;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public RelayCommand(Action<object?> execute) : this(execute, _ => true) {
            // noop
        }

        public RelayCommand(Action<object?> execute, Predicate<object?> canExecute) {
            _execute = execute;
            _canExecute = canExecute;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void Execute(object? parameter) {
            _execute(parameter);
        }

        public bool CanExecute(object? parameter) {
            return _canExecute(parameter);
        }
    }
}
