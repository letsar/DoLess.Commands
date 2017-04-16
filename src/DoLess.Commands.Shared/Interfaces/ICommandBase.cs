using System.ComponentModel;

namespace DoLess.Commands
{
    /// <summary>
    /// Represents a command.
    /// </summary>
    public interface ICommandBase : System.Windows.Input.ICommand, INotifyPropertyChanged
    {
        /// <summary>
        /// Indicates wether the command is executing.
        /// </summary>
        bool IsExecuting { get; }

        /// <summary>
        /// Raises the <see cref="System.Windows.Input.ICommand.CanExecuteChanged"/> event.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
