using DailyPlanner.Web.Pages.Notifications.Models;

namespace DailyPlanner.Web.Pages.Notifications.Services;

public interface INotificationService
{
    /// <summary>
    /// Retrieves all notifications for the current user.
    /// </summary>
    /// <returns>An enumerable collection of notifications.</returns>
    Task<IEnumerable<Notification>> GetNotifications();

    /// <summary>
    /// Marks a notification as read.
    /// </summary>
    /// <param name="notificationId">The ID of the notification to mark as read.</param>
    Task MarkAsRead(int notificationId);

    /// <summary>
    /// Marks all notifications as read for the current user.
    /// </summary>
    Task MarkAllAsRead();

    /// <summary>
    /// Deletes a notification.
    /// </summary>
    /// <param name="notificationId">The ID of the notification to delete.</param>
    Task DeleteNotification(int notificationId);

    /// <summary>
    /// Deletes all notifications for the current user.
    /// </summary>
    Task DeleteAllNotifications();
}