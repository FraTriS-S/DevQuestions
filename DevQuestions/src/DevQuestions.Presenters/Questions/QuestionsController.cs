using DevQuestions.Application.Questions;
using DevQuestions.Contracts.Questions;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Presenters.Questions;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionsService _questionsService;

    public QuestionsController(IQuestionsService questionsService)
    {
        _questionsService = questionsService;
    }

    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateQuestionDto request,
        CancellationToken cancellationToken)
    {
        var questionId = await _questionsService.Create(request, cancellationToken);
        return Ok(questionId);
    }

    // todo: доделать
    // [HttpGet("{questionId:guid}")]
    // public async Task<IActionResult> GetById(
    //     [FromRoute] Guid questionId,
    //     CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
    //
    // [HttpGet]
    // public async Task<IActionResult> Get(
    //     [FromQuery] GetQuestionsDto request,
    //     CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
    //
    // [HttpPut("{questionId:guid}")]
    // public async Task<IActionResult> Update(
    //     [FromRoute] Guid questionId,
    //     [FromBody] UpdateQuestionDto request,
    //     CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
    //
    // [HttpDelete("{questionId:guid}")]
    // public async Task<IActionResult> Delete(
    //     [FromRoute] Guid questionId,
    //     CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
    //
    // [HttpPost("{questionId:guid}/answers")]
    // public async Task<ActionResult> AddAnswer(
    //     [FromRoute] Guid questionId,
    //     [FromBody] AddAnswerDto request,
    //     CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
    //
    // [HttpPut("{questionId:guid}/solution")]
    // public async Task<IActionResult> SelectSolution(
    //     [FromRoute] Guid questionId,
    //     [FromQuery] Guid solutionId,
    //     CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
}