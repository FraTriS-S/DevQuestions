using CSharpFunctionalExtensions;
using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace DevQuestions.Infrastructure.PostgreSql.Repositories;

public class QuestionsRepository : IQuestionsRepository
{
    private readonly QuestionsDbContext _questionsDbContext;

    public QuestionsRepository(QuestionsDbContext questionsDbContext)
    {
        _questionsDbContext = questionsDbContext;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        await _questionsDbContext.AddAsync(question, cancellationToken);

        await _questionsDbContext.SaveChangesAsync(cancellationToken);

        return question.Id;
    }

    public Task<Guid> UpdateAsync(Question question, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<Result<Question, Failure>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var question = await _questionsDbContext.Questions
            .Include(x => x.Answers)
            .Include(x => x.Solution)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (question is null)
        {
            return Application.Questions.Fails.Errors.General.NotFound(id);
        }

        return question;
    }

    public Task<int> GetOpenedUserQuestionsCountAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
        _questionsDbContext.Attach(question);
        await _questionsDbContext.SaveChangesAsync(cancellationToken);
        return question.Id;
    }
}