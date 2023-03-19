namespace DailyPlanner.Services.RabbitMq;

/// <summary>
/// The action to perform when data is received from the queue.
/// </summary>
/// <typeparam name="T">The type of the data received from the queue.</typeparam>
/// <param name="data">The data received from the queue.</param>
/// <returns>A task representing the asynchronous operation.</returns>
public delegate Task OnDataReceive<T>(T data);

public interface IRabbitMq
{
    /// <summary>
    /// Subscribes to a RabbitMQ queue.
    /// </summary>
    /// <typeparam name="T">The type of the data received from the queue.</typeparam>
    /// <param name="queueName">The name of the queue to subscribe to.</param>
    /// <param name="onReceive">The action to perform when data is received from the queue.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Subscribe<T>(string queueName, OnDataReceive<T> onReceive);

    /// <summary>
    /// Pushes data of type T to a RabbitMQ queue.
    /// </summary>
    /// <typeparam name="T">The type of the data to push to the queue.</typeparam>
    /// <param name="queueName">The name of the queue to push the data to.</param>
    /// <param name="data">The data to push to the queue.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PushAsync<T>(string queueName, T data);
}