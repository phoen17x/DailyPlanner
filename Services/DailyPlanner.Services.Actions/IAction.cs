using DailyPlanner.Services.EmailSender.Models;

namespace DailyPlanner.Services.Actions;

public interface IAction
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="email">The email to be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendEmail(EmailModel email);
}