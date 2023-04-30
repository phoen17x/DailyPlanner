using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace DailyPlanner.Services.RabbitMq;

/// <summary>
/// Manages a RabbitMQ connection and provides methods for subscribing to and publishing messages on queues.
/// </summary>
public class RabbitMq : IRabbitMq, IDisposable
{
    private readonly object connectionLock = new();
    private readonly RabbitMqSettings settings;
    private IModel? channel;
    private IConnection? connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMq"/> class with the specified RabbitMQ settings.
    /// </summary>
    /// <param name="settings">The RabbitMQ settings to use for the connection.</param>
    public RabbitMq(RabbitMqSettings settings)
    {
        this.settings = settings;
    }

    public void Dispose()
    {
        channel?.Close();
        connection?.Close();
    }

    private IModel? GetChannel() => channel;

    private async Task RegisterListener(string queueName, EventHandler<BasicDeliverEventArgs> onReceive, int? messageLifetime = null)
    {
        Connect();
        AddQueue(queueName, messageLifetime);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += onReceive;
        channel.BasicConsume(queueName, false, consumer);
    }

    private async Task Publish<T>(string queueName, T data)
    {
        Connect();
        AddQueue(queueName);
        var json = JsonSerializer.Serialize<object>(data, new JsonSerializerOptions());
        var message = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(string.Empty, queueName, null, message);
    }

    private void Connect()
    {
        lock (connectionLock)
        {
            if (connection is not null && connection.IsOpen) return;

            var factory = new ConnectionFactory
            {
                Uri = new Uri(settings.Uri),
                UserName = settings.UserName,
                Password = settings.Password,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5)
            };

            var retriesCount = 0;
            while (retriesCount < 15)
                try
                {
                    connection ??= factory.CreateConnection();

                    if (channel == null)
                    {
                        channel = connection.CreateModel();
                        channel.BasicQos(0, 1, false);
                    }

                    break;
                }
                catch (BrokerUnreachableException)
                {
                    Task.Delay(1000).Wait();
                    retriesCount++;
                }
        }
    }

    private void AddQueue(string queueName, int? messageLifetime = null)
    {
        Connect();
        channel?.QueueDeclare(queueName, true, false, false, null);
    }

    public async Task Subscribe<T>(string queueName, OnDataReceive<T>? onReceive)
    {
        if (onReceive is null) return;

        await RegisterListener(queueName, async (_, eventArgs) =>
        {
            var channel = GetChannel();
            try
            {
                var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var obj = JsonSerializer.Deserialize<T>(message ?? "");

                await onReceive(obj);
                channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception e)
            {
                channel.BasicNack(eventArgs.DeliveryTag, false, false);
            }
        });
    }

    public async Task PushAsync<T>(string queueName, T data)
    {
        await Publish(queueName, data);
    }
}