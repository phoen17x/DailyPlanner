namespace DailyPlanner.Web.Pages.Notifications.Models;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime SendingTime { get; set; }
    public bool IsMarkedAsRead { get; set; }
}