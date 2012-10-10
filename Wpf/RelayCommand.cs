using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Windows.Input;

namespace Wanderer.Library.Wpf
{
    /// <summary>
    /// Simple implementation the <see cref="ICommand"/> interface and wraps up all the verbose stuff so that you can just pass two delegates:
    /// one for the <see cref="ICommand.CanExecute"/> and second for the <see cref="ICommand.Execute"/>.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Initialization constructor.
        /// </summary>
        /// <param name="execute">delegate for th <see cref="ICommand.Execute"/></param>
        /// <param name="canExecute">delegate for the <see cref="ICommand.CanExecute"/></param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            Contract.Requires<ArgumentNullException>(execute != null, "execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #region ICommand implementation
        /// <summary>
        /// Checks if the command method can run.
        /// </summary>
        /// <param name="parameter">the command parameter to be passed</param>
        /// <returns>
        /// Returns true if the command can be executed.
        /// By default true is returned so that if the user of <see cref="RelayCommand"/> does not specify a <see cref="ICommand.CanExecute"/> delegate the command still executes.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged { add { CommandManager.RequerySuggested += value; } remove { CommandManager.RequerySuggested -= value; } }

        /// <summary>
        /// Executes the actual command.
        /// </summary>
        /// <param name="parameter">the command parameter to be passed</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        #endregion
    }
}