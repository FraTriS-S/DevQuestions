using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Infrastructure.PostgreSql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Infrastructure.PostgreSql;

public static class DependencyInjection
{
    public static void AddPostgreSqlInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IQuestionsRepository, QuestionsRepository>();
        services.AddScoped<QuestionsDbContext>();
        services.AddScoped<ITransactionManager, TransactionManager>();
    }
}