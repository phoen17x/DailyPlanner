using AutoMapper;
using DailyPlanner.Services.Notifications.Models;

namespace DailyPlanner.Api.Controllers.Notifications.Models;

/// <summary>
/// Represents a response object for a notification.
/// </summary>
public class NotificationResponse
{
    /// <summary>
    /// Gets or sets the id of the notification.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the notification.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the notification.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sending time of the notification in string format.
    /// </summary>
    public string SendingTime { get; set; } = string.Empty;

    /// <summary>
    /// True if notification is marked as read, otherwise false.
    /// </summary>
    public bool IsMarkedAsRead { get; set; }
}

/// <summary>
/// Maps <see cref="NotificationModel"/> to <see cref="NotificationResponse"/>.
/// </summary>
public class NotificationResponseProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationResponseProfile"/> class.
    /// </summary>
    public NotificationResponseProfile()
    {
        CreateMap<NotificationModel, NotificationResponse>();
    }
}