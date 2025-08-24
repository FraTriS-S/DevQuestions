using DevQuestions.Application.Questions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Presenters.Extensions;
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
        var result = await _questionsService.Create(request, cancellationToken);

        return result.ToResponse();
    }
}