using DailyPlanner.Services.Notifications;

namespace DailyPlanner.Api.Configuration;

/// <summary>
/// Configures SignalR.
/// </summary>
public static class SignalRConfiguration
{
    /// <summary>
    /// Adds SignalR services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add SignalR services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAppSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }

    /// <summary>
    /// Maps the NotificationHub to the "/notificationHub" endpoint in the application.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to map the NotificationHub to.</param>
    public static void UseAppSignalR(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/notificationHub");
    }
}