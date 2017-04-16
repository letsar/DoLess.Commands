namespace DoLess.Commands
{
    /// <summary>
    /// Represents a command that can be cancelled.
    /// </summary>
    public interface ICancellableCommandBase
    {
        /// <summary>
        /// Gets the command used to cancel this.
        /// </summary>
        ICommand CancelCommand { get; }

        /// <summary>
        /// Indicates whether this command can be cancelled.
        /// </summary>
        /// <returns></returns>
        bool CanCancel();

        /// <summary>
        /// Cancels the execution.
        /// </summary>
        void Cancel();
    }
}