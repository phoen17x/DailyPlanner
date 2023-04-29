using AutoMapper;
using DailyPlanner.Context.Entities;
using static DailyPlanner.Common.Consts.DateTimeFormats;

namespace DailyPlanner.Services.Notifications.Models;

/// <summary>
/// Represents a model for a notification.
/// </summary>
public class NotificationModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SendingTime { get; set; } = string.Empty;
    public bool IsMarkedAsRead { get; set; }
}

/// <summary>
/// Maps <see cref="Notification"/> to <see cref="NotificationModel"/>.
/// </summary>
public class NotificationModelProfile : Profile
{
    public NotificationModelProfile()
    {
        CreateMap<Notification, NotificationModel>()
            .ForMember(dest => dest.SendingTime,
                options => options.MapFrom(src => src.SendingTime.ToString(DATE_TIME_WITHOUT_SECONDS)));
    }
}