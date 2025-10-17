using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Extensions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.CreateQuestion;

public class CreateQuestionHandler : ICommandHandler<CreateQuestionCommand, Guid>
{
    private readonly ILogger<CreateQuestionHandler> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;
    private readonly IQuestionsRepository _questionsRepository;

    public CreateQuestionHandler(
        ILogger<CreateQuestionHandler> logger,
        IValidator<CreateQuestionDto> validator,
        IQuestionsRepository questionsRepository)
    {
        _logger = logger;
        _validator = validator;
        _questionsRepository = questionsRepository;
    }

    public async Task<Result<Guid, Failure>> Handle(
        CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.CreateQuestionDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToFailure();
        }

        int openedUserQuestionsCount = await _questionsRepository
            .GetOpenedUserQuestionsCountAsync(command.CreateQuestionDto.UserId, cancellationToken);

        if (openedUserQuestionsCount > 3)
        {
            return Fails.Errors.Questions.TooManyQuestions();
        }

        var questionId = Guid.NewGuid();

        Question question = new(
            questionId,
            command.CreateQuestionDto.Title,
            command.CreateQuestionDto.Text,
            null,
            command.CreateQuestionDto.UserId,
            command.CreateQuestionDto.TagsIds);

        questionId = await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Question created with id {questionId}", questionId);

        return questionId;
    }
}