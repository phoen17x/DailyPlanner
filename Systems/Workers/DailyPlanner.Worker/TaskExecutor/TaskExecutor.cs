using DailyPlanner.Common.Consts;
using DailyPlanner.Services.EmailSender;
using DailyPlanner.Services.EmailSender.Models;
using DailyPlanner.Services.RabbitMq;

namespace DailyPlanner.Worker.TaskExecutor;

/// <summary>
/// Represents a task executor that subscribes to a RabbitMQ queue and executes actions for incoming messages.
/// </summary>
public class TaskExecutor : ITaskExecutor
{
    private readonly ILogger<TaskExecutor> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly IRabbitMq rabbitMq;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskExecutor"/> class.
    /// </summary>
    /// <param name="logger">The logger used for logging.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="rabbitMq">The RabbitMQ client used for subscribing to a queue.</param>

    public TaskExecutor(
        ILogger<TaskExecutor> logger,
        IServiceProvider serviceProvider,
        IRabbitMq rabbitMq)
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.rabbitMq = rabbitMq;
    }

    /// <summary>
    /// Executes the specified action with a service of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of service to resolve.</typeparam>
    /// <param name="action">The action to execute with the resolved service.</param>
    private async Task Execute<T>(Func<T, Task> action)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetService<T>();
            if (service != null) await action(service);
            else logger.LogError($"Error: {action} wasn't resolved");
        }
        catch (Exception e)
        {
            logger.LogError($"Error: {RabbitMqQueueNames.SEND_EMAIL}: {e.Message}");
            throw;
        }
    }

    public void Start()
    {
        rabbitMq.Subscribe<EmailModel>(
            RabbitMqQueueNames.SEND_EMAIL, 
            async data => await Execute<IEmailSender>(async service => {
                logger.LogDebug($"RabbitMQ: {RabbitMqQueueNames.SEND_EMAIL}: {data.Email} {data.Message}");
                await service.Send(data);
            }));
    }
}