using CSharpFunctionalExtensions;
using Shared;

namespace DevQuestions.Application.Abstractions;

public interface ICommand;

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand
{
    Task<Result<TResponse, Failure>> Handle(TCommand command, CancellationToken token);
}

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<UnitResult<Failure>> Handle(TCommand command, CancellationToken token);
}