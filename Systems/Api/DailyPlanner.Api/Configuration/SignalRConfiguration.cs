using DailyPlanner.Services.Notifications;

namespace DailyPlanner.Api.Configuration;

public static class SignalRConfiguration
{
    public static IServiceCollection AddAppSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }

    public static void UseAppSignalR(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/notificationHub");
    }
}