using CSharpFunctionalExtensions;
using DevQuestions.Application.Extensions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;
    private readonly IQuestionsRepository _questionsRepository;

    public QuestionsService(
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> validator,
        IQuestionsRepository questionsRepository)
    {
        _logger = logger;
        _validator = validator;
        _questionsRepository = questionsRepository;
    }

    public async Task<Result<Guid, Errors>> Create(CreateQuestionDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        int openedUserQuestionsCount = await _questionsRepository
            .GetOpenedUserQuestionsCountAsync(request.UserId, cancellationToken);

        if (openedUserQuestionsCount > 3)
        {
            return Fails.Errors.Questions.TooManyQuestions;
        }

        var questionId = Guid.NewGuid();

        Question question = new(
            questionId,
            request.Title,
            request.Text,
            null,
            request.UserId,
            request.TagsIds);

        questionId = await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Question created with id {questionId}", questionId);

        return questionId;
    }
}