namespace DoLess.Commands
{
    /// <summary>
    /// Represents a command with a parameter that can be cancelled.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    public interface ICancellableCommand<T> : ICancellableCommandBase, ICommand<T>
    {
    }
}
