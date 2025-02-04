namespace Elegant.Abstraction.Handlers.Command;

[PublicAPI]
public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}

[PublicAPI]
public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}