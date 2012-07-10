using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Windows.Input;

namespace Wanderer.Library.Wpf
{
    // TODO: Write xml comments & check contracts
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            Contract.Requires<ArgumentNullException>(execute != null, "execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #region ICommand implementation
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged { add { CommandManager.RequerySuggested += value; } remove { CommandManager.RequerySuggested -= value; } }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        #endregion
    }
}