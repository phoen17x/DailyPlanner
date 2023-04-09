using System.Text;

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
        if (StartTime.ToShortDateString() == EstimatedCompletionTime.ToShortDateString())
        {
            if (DateTime.Today.ToShortDateString() == StartTime.ToShortDateString())
                return $"Today {StartTime:HH:mm} - {EstimatedCompletionTime:HH:mm}";
            
            if (DateTime.Today.AddDays(1).ToShortDateString() == StartTime.ToShortDateString())
                return $"Tomorrow {StartTime:HH:mm} - {EstimatedCompletionTime:HH:mm}";

            return $"{StartTime.ToString("d MMMM yyyy")} {StartTime:HH:mm} - {EstimatedCompletionTime:HH:mm}";
        }

        if (DateTime.Today.ToShortDateString() == StartTime.ToShortDateString() &&
            DateTime.Today.AddDays(1).ToShortDateString() == StartTime.ToShortDateString())
            return $"Today {StartTime:HH:mm} - Tomorrow {EstimatedCompletionTime:HH:mm}";

        if (DateTime.Today.AddDays(1).ToShortDateString() == StartTime.ToShortDateString())
            return $"Tomorrow {StartTime:HH:mm} - {EstimatedCompletionTime.ToString("d MMMM yyyy")} {EstimatedCompletionTime:HH:mm}";

        return $"{StartTime.ToString("d MMMM yyyy")} {StartTime:HH:mm} - {EstimatedCompletionTime.ToString("d MMMM yyyy")} {EstimatedCompletionTime:HH:mm}";
    }
}   

public enum TodoTaskStatus
{
    Scheduled,
    Pending,
    Completed,
    Cancelled
}