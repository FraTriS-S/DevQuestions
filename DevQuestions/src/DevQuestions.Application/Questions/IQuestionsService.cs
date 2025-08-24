using CSharpFunctionalExtensions;
using DevQuestions.Contracts.Questions;
using Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionsService
{
    Task<Result<Guid, Errors>> Create(CreateQuestionDto request, CancellationToken cancellationToken);
}