using DevQuestions.Application;
using DevQuestions.Infrastructure.PostgreSql;

namespace DevQuestions.Web;

public static class DependencyInjection
{
    public static void AddProgramDependencies(this IServiceCollection services)
    {
        services.AddWebDependencies();
        services.AddApplication();
        services.AddPostgreSqlInfrastructure();
    }

    private static void AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();
    }
}