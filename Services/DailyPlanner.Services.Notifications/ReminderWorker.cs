using Microsoft.Extensions.Hosting;

namespace DailyPlanner.Services.Notifications;

public class ReminderWorker : BackgroundService
{
    private readonly INotificationService notificationService;

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
