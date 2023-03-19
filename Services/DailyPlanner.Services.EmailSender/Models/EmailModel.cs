namespace DailyPlanner.Services.EmailSender.Models;

/// <summary>
/// Represents an email model.
/// </summary>
public class EmailModel
{
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}