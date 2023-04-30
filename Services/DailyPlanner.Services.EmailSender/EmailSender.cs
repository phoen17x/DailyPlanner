using DailyPlanner.Services.EmailSender.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace DailyPlanner.Services.EmailSender;

/// <summary>
/// Provides an implementation of <see cref="IEmailSender"/> that sends email messages.
/// </summary>
public class EmailSender : IEmailSender
{
    private readonly EmailSenderSettings settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailSender"/> class.
    /// </summary>
    /// <param name="settings">The RabbitMQ settings to use for the connection.</param>
    public EmailSender(EmailSenderSettings settings)
    {
        this.settings = settings;
    }

    public async Task SendEmailAsync(EmailModel model)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(settings.Email);
        email.To.Add(MailboxAddress.Parse(model.Email));
        email.Subject = model.Subject;

        var builder = new BodyBuilder { HtmlBody = model.Message };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(settings.Host, settings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(settings.Email, settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}