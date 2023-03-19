using DailyPlanner.Services.EmailSender.Models;
using Microsoft.Extensions.Logging;

namespace DailyPlanner.Services.EmailSender;

/// <summary>
/// Provides an implementation of <see cref="IEmailSender"/> that sends email messages.
/// </summary>
public class EmailSender : IEmailSender
{
    /// <summary>
    /// The logger used to log email sending events.
    /// </summary>
    private ILogger<EmailSender> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailSender"/> class.
    /// </summary>
    /// <param name="logger">The logger to use for logging email sending events.</param>
    public EmailSender(ILogger<EmailSender> logger)
    {
        Logger = logger;
    }
    public async Task Send(EmailModel model)
    {
        await Task.Delay(2000);
        Logger.LogDebug($"Email was sent: {model.Email} {model.Subject} {model.Message}");
    }
}