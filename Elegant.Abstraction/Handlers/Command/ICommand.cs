namespace Elegant.Abstraction.Handlers.Command;

[PublicAPI]
public interface ICommand;

[PublicAPI]
public interface ICommand<TResult> : ICommand;