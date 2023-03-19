namespace DailyPlanner.Services.RabbitMq;

/// <summary>
/// Represents the RabbitMQ settings.
/// </summary>
public class RabbitMqSettings
{
    public string Uri { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
}