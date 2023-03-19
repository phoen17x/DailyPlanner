using DailyPlanner.Common.Consts;
using DailyPlanner.Services.EmailSender.Models;
using DailyPlanner.Services.RabbitMq;

namespace DailyPlanner.Services.Actions;

/// <summary>
/// Provides actions for RabbitMQ.
/// </summary>
public class Action : IAction
{
    private readonly IRabbitMq rabbitMq;

    /// <summary>
    /// Initializes a new instance of the <see cref="Action"/> class.
    /// </summary>
    /// <param name="rabbitMq">The RabbitMQ instance.</param>
    public Action(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    public async Task SendEmail(EmailModel email)
    {
        await rabbitMq.PushAsync(RabbitMqQueueNames.SEND_EMAIL, email);
    }
}