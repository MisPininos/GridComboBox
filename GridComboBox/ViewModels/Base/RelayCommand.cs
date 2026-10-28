using System.Windows.Input;

namespace GridComboBox.ViewModels.Base
{
    class RelayCommand : ICommand
    {
        private readonly Action? _action;
        private readonly Action<object?>? _actionWithParameter;
        private readonly Func<bool>? _canExecute;

        // Constructor for Action (no parameter)
        public RelayCommand(Action action, Func<bool>? canExecute = null)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _actionWithParameter = null;
            _canExecute = canExecute;
        }

        // Constructor for Action<object?> (with parameter)
        public RelayCommand(Action<object?> action, Func<bool>? canExecute = null)
        {
            _actionWithParameter = action ?? throw new ArgumentNullException(nameof(action));
            _action = null;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object? parameter)
        {
            if (_actionWithParameter != null)
            {
                _actionWithParameter(parameter);
            }
            else
            {
                _action?.Invoke();
            }
        }
    }
}
