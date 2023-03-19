using DailyPlanner.Services.EmailSender.Models;

namespace DailyPlanner.Services.EmailSender;

public interface IEmailSender
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="email">The email to send.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Send(EmailModel email);
}