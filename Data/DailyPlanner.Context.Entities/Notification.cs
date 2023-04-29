using DailyPlanner.Context.Entities.Common;

namespace DailyPlanner.Context.Entities;

/// <summary>
/// Represents a Notification entity.
/// </summary>
public class Notification : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime SendingTime { get; set; }
    public bool IsMarkedAsRead { get; set; }
    public Guid UserId { get; set; }
    public virtual User.User User { get; set; }
}