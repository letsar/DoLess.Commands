using System.Threading.Tasks;

namespace DoLess.Commands
{
    /// <summary>
    /// Represents a command without parameter.
    /// </summary>
    public interface ICommand : ICommandBase
    {
        /// <summary>
        /// Indicates whether this command can be executed.
        /// </summary>
        /// <returns></returns>
        bool CanExecute();

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();
    }
}
