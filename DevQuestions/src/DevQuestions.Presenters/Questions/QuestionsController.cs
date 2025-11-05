using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Questions.Features.AddAnswer;
using DevQuestions.Application.Questions.Features.CreateQuestion;
using DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Presenters.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Presenters.Questions;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] ICommandHandler<CreateQuestionCommand, Guid> handler,
        [FromBody] CreateQuestionDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuestionCommand(request);
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromServices] IQueryHandler<GetQuestionsWithFiltersQuery, QuestionResponse> handler,
        [FromQuery] GetQuestionsDto request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuestionsWithFiltersQuery(request);
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{questionId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid questionId,
        CancellationToken cancellationToken)
    {
        return Ok("Question get");
    }

    [HttpPut("{questionId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid questionId,
        [FromBody] UpdateQuestionDto request,
        CancellationToken cancellationToken)
    {
        return Ok("Question updated");
    }

    [HttpDelete("{questionId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid questionId,
        CancellationToken cancellationToken)
    {
        return Ok("Question deleted");
    }

    [HttpPut("{questionId:guid}/solution")]
    public async Task<IActionResult> SelectSolution(
        [FromRoute] Guid questionId,
        [FromQuery] Guid answerId,
        CancellationToken cancellationToken)
    {
        return Ok("Solution selected");
    }

    [HttpPost("{questionId:guid}/answers")]
    public async Task<IActionResult> AddAnswer(
        [FromServices] ICommandHandler<AddAnswerCommand, Guid> handler,
        [FromRoute] Guid questionId,
        [FromQuery] AddAnswerDto request,
        CancellationToken cancellationToken)
    {
        var command = new AddAnswerCommand(questionId, request);
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }
}