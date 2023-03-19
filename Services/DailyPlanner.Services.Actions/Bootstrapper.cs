using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Services.Actions;

public static class Bootstrapper
{
    public static IServiceCollection AddActions(this IServiceCollection services)
    {
        services.AddSingleton<IAction, Action>();
        return services;
    }
}