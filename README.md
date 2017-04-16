![Logo](./nuget/doless.png)

**DoLess.Commands** is a simple implementation of `System.Windows.Input.ICommand`. It can be used independently of any other MVVM framework.

## Why?
Here are the main features of **DoLess.Commands**:

- Create commands from `Action`, `Action<T>`, `Func<Task>`, `Func<CancellationToken, Task`, `Func<T, Task>`, `Func<T, CancellationToken, Task`.
- The execution logic runs on the same thread as the caller (typically the UI Thread).
- While the command is running, it cannot be executed.
- If a command is created with a `CancellationToken`, the command can be cancelled with `CancelCommand`.

## Install

Available on NuGet soon.

## Usage

From an action:
```csharp
private void InitializeCommands()
{
	this.SearchCommand = Command.CreateFromAction(this.Search);
}

private void Search()
{
	// Some code.
}
```

From a task-based method:
```csharp
private void InitializeCommands()
{
	this.SearchCommand = Command.CreateFromTask(this.SearchAsync);
}

private Task SearchAsync(CancellationToken ct)
{
	// Some code.
}
```
