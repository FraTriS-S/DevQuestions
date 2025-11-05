using DevQuestions.Application.Tags;
using DevQuestions.Domain.Tags;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure.PostgreSql.Tags;

public class TagsDbContext : DbContext, ITagsReadDbContext
{
    public DbSet<Tag> Tags => Set<Tag>();

    public IQueryable<Tag> TagsRead => Tags.AsNoTracking().AsQueryable();
}