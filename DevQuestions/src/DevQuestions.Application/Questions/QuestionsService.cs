using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;

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

    public async Task<Guid> Create(CreateQuestionDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        int openedUserQuestionsCount = await _questionsRepository.GetOpenedUserQuestionsCountAsync(
            request.UserId, cancellationToken);

        if (openedUserQuestionsCount > 3)
        {
            throw new Exception("Пользователь не может открыть больше 3 вопросов");
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

    // todo: доделать
    // public async Task Update(
    //     Guid questionId,
    //     UpdateQuestionDto request,
    //     CancellationToken cancellationToken)
    // {
    //     var question = await _questionsRepository.GetByIdAsync(questionId, cancellationToken);
    //
    //     await _questionsRepository.UpdateAsync(question, cancellationToken);
    //
    //     _logger.LogInformation("Question updated with id {questionId}", questionId);
    // }
    //
    // public async Task Delete(
    //     Guid questionId,
    //     CancellationToken cancellationToken)
    // {
    //     var question = await _questionsRepository.DeleteAsync(questionId, cancellationToken);
    //
    //     _logger.LogInformation("Question deleted with id {questionId}", questionId);
    // }
    //
    // public async Task AddAnswer(
    //     Guid questionId,
    //     AddAnswerDto request,
    //     CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task SelectSolution(
    //     Guid questionId,
    //     Guid solutionId,
    //     CancellationToken cancellationToken)
    // {
    // }
}