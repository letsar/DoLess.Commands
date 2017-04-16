using System;
using System.Threading;
using System.Threading.Tasks;

namespace DoLess.Commands.Implementations
{
    /// <summary>
    /// Represents a command that can be cancelled.
    /// </summary>
    internal class CancellableCommand<T> : Command<T>, ICancellableCommand<T>
    {
        public CancellableCommand(Func<T, CancellationToken, Task> execute, Func<T, bool> canExecute = null) : base(execute, canExecute)
        {
            this.CancelCommand = Commands.Command.CreateFromAction(this.Cancel, this.CanCancel);
        }
    }
}