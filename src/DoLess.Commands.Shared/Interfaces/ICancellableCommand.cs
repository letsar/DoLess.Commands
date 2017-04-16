namespace DoLess.Commands
{
    /// <summary>
    /// Represents a command without a parameter that can be cancelled.
    /// </summary>
    public interface ICancellableCommand : ICancellableCommandBase, ICommand
    {
    }
}
