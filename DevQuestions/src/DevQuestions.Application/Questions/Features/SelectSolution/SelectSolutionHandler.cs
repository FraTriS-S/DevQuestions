using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using Shared;

namespace DevQuestions.Application.Questions.Features.SelectSolution;

public class SelectSolutionHandler : ICommandHandler<SelectSolutionCommand, Guid>
{
    public Task<Result<Guid, Failure>> Handle(SelectSolutionCommand command, CancellationToken token)
        => throw new NotImplementedException();
}