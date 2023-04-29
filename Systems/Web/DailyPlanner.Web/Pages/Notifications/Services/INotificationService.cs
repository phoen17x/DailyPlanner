using DailyPlanner.Web.Pages.Notifications.Models;

namespace DailyPlanner.Web.Pages.Notifications.Services;

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetNotifications();
    Task MarkAsRead(int notificationId);
    Task MarkAllAsRead();
    Task DeleteNotification(int notificationId);
    Task DeleteAllNotifications();
}