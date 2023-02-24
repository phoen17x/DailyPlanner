using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Services.Notebooks;

public static class Bootstrapper
{
    public static IServiceCollection AddNotebookService(this IServiceCollection services)
    {
        services.AddSingleton<INotebookService, NotebookService>();
        return services;
    }
}