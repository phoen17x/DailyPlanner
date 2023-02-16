using System.ComponentModel.DataAnnotations;
using DailyPlanner.Context.Entities.Common;

namespace DailyPlanner.Context.Entities;

/// <summary>
/// Represents a TodoTask entity.
/// </summary>
public class TodoTask : BaseEntity
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public DateTime StartTime { get; set;}
    [Required]
    public DateTime EstimatedCompletionTime { get; set; }
    public DateTime ActualCompletionTime { get; set; }
    [Required]
    public TodoTaskStatus Status { get; set; }
    public virtual Notebook Notebook { get; set; }
}

/// <summary>
/// Represents the status of a TodoTask.
/// </summary>
public enum TodoTaskStatus
{
    Scheduled,
    Pending,
    Completed,
    Cancelled,
    Deleted
}