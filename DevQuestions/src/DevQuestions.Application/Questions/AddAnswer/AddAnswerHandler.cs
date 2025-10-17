using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Communication;
using DevQuestions.Application.Database;
using DevQuestions.Application.Extensions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.AddAnswer;

public class AddAnswerHandler : ICommandHandler<AddAnswerCommand, Guid>
{
    private readonly ILogger<AddAnswerHandler> _logger;
    private readonly IValidator<AddAnswerDto> _validator;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ITransactionManager _transactionManager;
    private readonly IUsersCommunicationService _usersCommunicationService;

    public AddAnswerHandler(
        ILogger<AddAnswerHandler> logger,
        IValidator<AddAnswerDto> validator,
        IQuestionsRepository questionsRepository,
        ITransactionManager transactionManager,
        IUsersCommunicationService usersCommunicationService)
    {
        _logger = logger;
        _validator = validator;
        _questionsRepository = questionsRepository;
        _transactionManager = transactionManager;
        _usersCommunicationService = usersCommunicationService;
    }

    public async Task<Result<Guid, Failure>> Handle(
        AddAnswerCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.AddAnswerDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToFailure();
        }

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        var userRatingResult = await _usersCommunicationService.GetUSerRatingAsync(
            command.AddAnswerDto.UserId, cancellationToken);

        if (userRatingResult.IsFailure)
        {
            return userRatingResult.Error;
        }

        if (userRatingResult.Value <= 0)
        {
            _logger.LogError("User with id {userId} has rating too low", command.AddAnswerDto.UserId);
            return Fails.Errors.Questions.NotEnoughRating();
        }

        var questionResult = await _questionsRepository.GetByIdAsync(command.QuestionId, cancellationToken);

        if (questionResult.IsFailure)
        {
            return questionResult.Error;
        }

        var question = questionResult.Value;

        var answer = new Answer(
            Guid.NewGuid(),
            command.AddAnswerDto.UserId,
            command.AddAnswerDto.Text,
            command.QuestionId);

        question.Answers.Add(answer);

        await _questionsRepository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation(
            "Answer added with id {answerId} to question with id {questionId}", answer.Id, command.QuestionId);

        return answer.Id;
    }
}