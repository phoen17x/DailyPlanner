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
        var startWord = StartTime > DateTime.Now ? "Starts" : "Started";

        if (DateTime.Today.ToShortDateString() == StartTime.ToShortDateString())
            return $"{startWord} today at {StartTime:h:mm tt}";

        if (DateTime.Today.AddDays(1).ToShortDateString() == StartTime.ToShortDateString())
            return $"{startWord} tomorrow at {StartTime:h:mm tt}";

        return $"{startWord} {StartTime.ToString("MMMM d, yyyy")} at {StartTime:h:mm tt}";
    }

    public string DateTimeToString()
    {
        return $"{StartTime:MMM d, yyyy, h:mm tt} - {EstimatedCompletionTime:MMM d, yyyy, h:mm tt}";
    }
}   

public enum TodoTaskStatus
{
    Scheduled,
    Completed,
    Canceled
}