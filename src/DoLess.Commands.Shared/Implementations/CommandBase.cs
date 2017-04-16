using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DoLess.Commands.Implementations
{
    /// <summary>
    /// Represents the base of the commands.
    /// </summary>
    internal abstract class CommandBase : ICommandBase
    {
        private readonly object ctsLock = new object();
        private CancellationTokenSource cts;        
        private bool isExecuting;

        public event EventHandler CanExecuteChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CancelCommand { get; protected set; }

        public bool IsExecuting
        {
            get { return this.isExecuting; }
            protected set { this.Set(ref this.isExecuting, value); }
        }

        bool System.Windows.Input.ICommand.CanExecute(object parameter)
        {
            return this.ICommandCanExecute(parameter);
        }

        async void System.Windows.Input.ICommand.Execute(object parameter)
        {
            try
            {
                await this.ICommandExecuteAsync(parameter).ConfigureAwait(false);
            }
            catch (Exception)
            {
                // Don't throw the exception.
                // If an exception occured, this one must be managed into the execution logic.
                // Raising an exception into this method can result to crash.
            }
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            this.CancelCommand?.RaiseCanExecuteChanged();
        }

        public bool CanCancel()
        {
            return this.cts != null;
        }

        public void Cancel()
        {
            lock (this.ctsLock)
            {
                this.cts?.Cancel();
            }
        }

        protected abstract bool CanExecute(object parameter);

        protected abstract Task ExecuteAsync(object parameter, CancellationToken ct);

        protected bool ICommandCanExecute(object parameter)
        {
            return !this.IsExecuting && this.CanExecute(parameter);
        }

        protected async Task ICommandExecuteAsync(object parameter)
        {
            if (this.ICommandCanExecute(parameter))
            {
                this.cts = new CancellationTokenSource();
                this.ChangeExecutionState(true);

                try
                {
                    // Executes the command on the same thread (typically the UI Thread).
                    await this.ExecuteAsync(parameter, cts.Token);
                }
                catch (Exception ex)
                {
                    // Don't bother to throw because, it will be silently catch in ICommand.Execute.
                }
                finally
                {
                    lock (this.ctsLock)
                    {
                        this.cts = null;
                    }
                    this.ChangeExecutionState(false);
                }
            }
        }

        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }
            field = newValue;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        private void ChangeExecutionState(bool state)
        {
            this.IsExecuting = state;
            this.RaiseCanExecuteChanged();
        }
    }
}