using System;
using System.Threading;
using System.Threading.Tasks;

namespace DoLess.Commands.Implementations
{
    /// <summary>
    /// Represents a command.
    /// </summary>
    internal class Command : CommandBase, ICommand
    {
        private readonly Func<bool> canExecute;
        private readonly Func<CancellationToken, Task> execute;

        public Command(Func<CancellationToken, Task> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute()
        {
            return this.ICommandCanExecute(null);
        }

        public Task ExecuteAsync()
        {
            return this.ICommandExecuteAsync(null);
        }

        protected override bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute();
        }

        protected override Task ExecuteAsync(object parameter, CancellationToken ct)
        {
            return this.execute(ct);
        }
    }
}