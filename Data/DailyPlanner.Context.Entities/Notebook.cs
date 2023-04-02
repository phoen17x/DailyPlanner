using DailyPlanner.Context.Entities.Common;

namespace DailyPlanner.Context.Entities;

/// <summary>
/// Represents a Notebook entity containing TodoTasks.
/// </summary>
public class Notebook : BaseEntity
{
    public string Title { get; set; }
    public virtual ICollection<TodoTask> TodoTasks { get; set; }
    public Guid UserId { get; set; }
    public virtual User.User User { get; set; }
}