using DevQuestions.Application;

namespace DevQuestions.Web;

public static class DependencyInjection
{
    public static void AddProgramDependencies(this IServiceCollection services)
    {
        services.AddWebDependencies();
        services.AddApplication();
    }

    private static void AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();
    }
}