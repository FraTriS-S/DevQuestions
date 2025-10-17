using DevQuestions.Application.Communication;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Infrastructure.Communication;

public static class DependencyInjection
{
    public static void AddCommunicationService(this IServiceCollection services)
    {
        services.AddTransient<IUsersCommunicationService, UsersCommunicationService>();
    }
}