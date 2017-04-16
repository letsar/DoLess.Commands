using System;
using System.Threading;
using System.Threading.Tasks;

namespace DoLess.Commands.Implementations
{
    /// <summary>
    /// Represents a command.
    /// </summary>
    internal class Command<T> : CommandBase, ICommand<T>
    {
        private readonly Func<T, bool> canExecute;
        private readonly Func<T, CancellationToken, Task> execute;

        public Command(Func<T, CancellationToken, Task> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(T parameter)
        {
            return this.ICommandCanExecute(parameter);
        }

        public Task ExecuteAsync(T parameter)
        {
            return this.ICommandExecuteAsync(parameter);
        }

        protected override bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }

            if (parameter == null)
            {
                return this.canExecute(default(T));
            }

            if (parameter is T)
            {
                return this.canExecute((T)parameter);
            }

            return false;
        }

        protected override Task ExecuteAsync(object parameter, CancellationToken ct)
        {
            T param = default(T);
            if (parameter != null && parameter is T)
            {
                param = (T)parameter;
            }

            return this.execute(param, ct);
        }
    }
}