using System.Threading.Tasks;

namespace DoLess.Commands
{
    /// <summary>
    /// Represents a command with a parameter.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    public interface ICommand<T> : ICommandBase
    {
        /// <summary>
        /// Indicates whether this command can be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        bool CanExecute(T parameter);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        Task ExecuteAsync(T parameter);
    }
}
