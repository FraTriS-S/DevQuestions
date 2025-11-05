using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Tags;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;

public class GetQuestionsWithFilters : IQueryHandler<GetQuestionsWithFiltersQuery, QuestionResponse>
{
    private readonly IQuestionsReadDbContext _questionsDbContext;
    private readonly ITagsReadDbContext _tagsDbContext;

    public GetQuestionsWithFilters(IQuestionsReadDbContext questionsDbContext, ITagsReadDbContext tagsDbContext)
    {
        _questionsDbContext = questionsDbContext;
        _tagsDbContext = tagsDbContext;
    }

    public async Task<QuestionResponse> Handle(GetQuestionsWithFiltersQuery query, CancellationToken cancellationToken)
    {
        var questions = await _questionsDbContext.ReadQuestions
            .Include(x => x.Solution)
            .Skip(query.Dto.Page * query.Dto.PageSize)
            .Take(query.Dto.PageSize)
            .ToListAsync(cancellationToken);

        long count = await _questionsDbContext.ReadQuestions.LongCountAsync(cancellationToken);

        var questionTags = questions.SelectMany(x => x.TagsIds).Distinct();

        var tags = await _tagsDbContext.TagsRead
            .Where(x => questionTags.Contains(x.Id))
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        var questionsDtos = questions.Select(x => new QuestionDto(
            x.Id,
            x.Title,
            x.Text,
            x.UserId,
            "screenshotUrl",
            x.Solution?.Id,
            tags,
            x.Status.ToRuString()));

        return new QuestionResponse(questionsDtos, count);
    }
}