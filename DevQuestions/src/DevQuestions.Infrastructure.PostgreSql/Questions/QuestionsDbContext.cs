using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure.PostgreSql.Questions;

public class QuestionsDbContext : DbContext, IQuestionsReadDbContext
{
    public DbSet<Question> Questions => Set<Question>();

    public IQueryable<Question> ReadQuestions => Questions.AsNoTracking().AsQueryable();
}