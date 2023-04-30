using Microsoft.Extensions.Hosting;

namespace DailyPlanner.Services.Notifications;

/// <summary>
/// A background service that periodically adds reminders for todotasks.
/// </summary>
public class ReminderWorker : BackgroundService
{
    private readonly INotificationService notificationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReminderWorker"/> class.
    /// </summary>
    /// <param name="notificationService">The notification service.</param>
    public ReminderWorker(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested == false)
        {
            await notificationService.AddTodoTaskReminders();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
