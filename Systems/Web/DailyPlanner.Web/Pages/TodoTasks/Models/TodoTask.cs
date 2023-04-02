namespace DailyPlanner.Web.Pages.TodoTasks.Models;

public class TodoTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EstimatedCompletionTime { get; set; }
    public DateTime ActualCompletionTime { get; set; }
    public TodoTaskStatus Status { get; set; }
    public int NotebookId { get; set; }
}

public enum TodoTaskStatus
{
    Scheduled,
    Pending,
    Completed,
    Cancelled
}