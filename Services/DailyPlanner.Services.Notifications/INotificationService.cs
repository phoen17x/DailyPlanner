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

    /// <summary>
    /// Adds reminders for tasks.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddTodoTaskReminders();

    /// <summary>
    /// Marks a notification as read for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier for the user.</param>
    /// <param name="notificationId">The unique identifier for the notification to mark as read.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task MarkAsRead(Guid userId, int notificationId);

    /// <summary>
    /// Marks all notifications as read for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier for the user.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task MarkAllAsRead(Guid userId);

    /// <summary>
    /// Deletes a notification for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier for the user.</param>
    /// <param name="notificationId">The unique identifier for the notification to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteNotification(Guid userId, int notificationId);

    /// <summary>
    /// Deletes all notifications for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier for the user.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAllNotifications(Guid userId);
}