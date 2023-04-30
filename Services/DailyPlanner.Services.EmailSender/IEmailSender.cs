using DailyPlanner.Services.EmailSender.Models;

namespace DailyPlanner.Services.EmailSender;

/// <summary>
/// Provides an email service.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="email">The email to send.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendEmailAsync(EmailModel email);
}