using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Services.Notifications;

public static class Bootstrapper
{
    public static IServiceCollection AddNotificationService(this IServiceCollection services)
    {
        services
            .AddSingleton<INotificationService, NotificationService>()
            .AddHostedService<ReminderWorker>();

        return services;
    }
}