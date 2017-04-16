using System;
using System.Threading;
using System.Threading.Tasks;

namespace DoLess.Commands.Implementations
{
    /// <summary>
    /// Represents a command that can be cancelled.
    /// </summary>
    internal class CancellableCommand : Command, ICancellableCommand
    {
        public CancellableCommand(Func<CancellationToken, Task> execute, Func<bool> canExecute = null) : base(execute, canExecute)
        {
            this.CancelCommand = Commands.Command.CreateFromAction(this.Cancel, this.CanCancel);
        }
    }
}