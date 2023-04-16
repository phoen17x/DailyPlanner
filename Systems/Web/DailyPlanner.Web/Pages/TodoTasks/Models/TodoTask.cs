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

    public string StartTimeToString()
    {
        if (DateTime.Today.ToShortDateString() == StartTime.ToShortDateString())
            return $"Starts today at {StartTime:HH:mm}";

        if (DateTime.Today.AddDays(1).ToShortDateString() == StartTime.ToShortDateString())
            return $"Starts tomorrow at {StartTime:HH:mm}";

        return $"Starts {StartTime.ToString("MMMM d, yyyy")} at {StartTime:HH:mm}";
    }

    public string DateTimeToString()
    {
        return $"{StartTime:MMM d, yyyy, h:mm tt} - {EstimatedCompletionTime:MMM d, yyyy, h:mm tt}";
    }
}   

public enum TodoTaskStatus
{
    Scheduled,
    Pending,
    Completed,
    Canceled
}