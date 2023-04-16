using DailyPlanner.Context.Entities.Common;

namespace DailyPlanner.Context.Entities;

/// <summary>
/// Represents a TodoTask entity.
/// </summary>
public class TodoTask : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set;}
    public DateTime EstimatedCompletionTime { get; set; }
    public DateTime ActualCompletionTime { get; set; }
    public TodoTaskStatus Status { get; set; }
    public int NotebookId { get; set; }
    public virtual Notebook Notebook { get; set; }
    public Guid UserId { get; set; }
    public virtual User.User User { get; set; }
}

/// <summary>
/// Represents the status of a TodoTask.
/// </summary>
public enum TodoTaskStatus
{
    Scheduled,
    Pending,
    Completed,
    Canceled
}