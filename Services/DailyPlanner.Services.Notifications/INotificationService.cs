using DailyPlanner.Services.Notifications.Models;

namespace DailyPlanner.Services.Notifications;

public interface INotificationService
{
    /// <summary>
    /// Gets a collection of notifications.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <returns>Collection of <see cref="NotificationModel"/> objects.</returns>
    Task<IEnumerable<NotificationModel>> GetNotifications(Guid userId);

    /// <summary>
    /// Adds a new notification.
    /// </summary>
    /// <param name="model">Data for the new notification.</param>
    /// <returns>The newly created <see cref="NotificationModel"/> object.</returns>
    Task<NotificationModel> AddNotification(AddNotificationModel model);
    
    Task AddTodoTaskReminders();
    Task MarkAsRead(Guid userId, int notificationId);
    Task MarkAllAsRead(Guid userId);
    Task DeleteNotification(Guid userId, int notificationId);
    Task DeleteAllNotifications(Guid userId);
}