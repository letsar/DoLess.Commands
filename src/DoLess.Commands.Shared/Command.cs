using System;
using System.Threading;
using System.Threading.Tasks;

namespace DoLess.Commands
{
    /// <summary>
    /// Contains methods used to create commands.
    /// </summary>
    public static class Command
    {
        /// <summary>
        /// Creates an <see cref="ICommand"/> with asynchronous execution logic.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <returns></returns>
        public static ICommand CreateFromAction(Action execute, Func<bool> canExecute = null)
        {
            EnsureExecuteNotNull(execute);
            return new Implementations.Command(ct => RunOnSameSynchronizationContext(execute, ct), canExecute);
        }

        /// <summary>
        /// Creates an <see cref="ICommand{T}"/> with asynchronous execution logic that takes a parameter of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the data used by the execution logic.</typeparam>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <returns></returns>
        public static ICommand<T> CreateFromAction<T>(Action<T> execute, Func<T, bool> canExecute = null)
        {
            EnsureExecuteNotNull(execute);
            return new Implementations.Command<T>((x, ct) => RunOnSameSynchronizationContext(() => execute(x), ct), canExecute);
        }

        /// <summary>
        /// Creates an <see cref="ICommand"/> with asynchronous execution logic.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <returns></returns>
        public static ICommand CreateFromTask(Func<Task> execute, Func<bool> canExecute = null)
        {
            EnsureExecuteNotNull(execute);
            return new Implementations.Command(ct => execute(), canExecute);
        }

        /// <summary>
        /// Creates an <see cref="ICancellableCommand"/> with asynchronous execution logic.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <returns></returns>
        public static ICancellableCommand CreateFromTask(Func<CancellationToken, Task> execute, Func<bool> canExecute = null)
        {
            EnsureExecuteNotNull(execute);
            return new Implementations.CancellableCommand(execute, canExecute);
        }

        /// <summary>
        /// Creates an <see cref="ICommand{T}"/> with asynchronous execution logic that takes a parameter of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the data used by the execution logic.</typeparam>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <returns></returns>
        public static ICommand<T> CreateFromTask<T>(Func<T, Task> execute, Func<T, bool> canExecute = null)
        {
            EnsureExecuteNotNull(execute);
            return new Implementations.Command<T>((x, ct) => execute(x), canExecute);
        }

        /// <summary>
        /// Creates an <see cref="ICancellableCommand{T}"/> with asynchronous execution logic that takes a parameter of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the data used by the execution logic.</typeparam>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <returns></returns>
        public static ICancellableCommand<T> CreateFromTask<T>(Func<T, CancellationToken, Task> execute, Func<T, bool> canExecute = null)
        {
            EnsureExecuteNotNull(execute);
            return new Implementations.CancellableCommand<T>(execute, canExecute);
        }

        private static Task RunOnSameSynchronizationContext(Action execute, CancellationToken ct)
        {
            return Task.Factory.StartNew(execute, ct, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private static void EnsureExecuteNotNull(Delegate execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
        }
    }
}
