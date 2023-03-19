namespace DailyPlanner.Worker.TaskExecutor;

public interface ITaskExecutor
{
    /// <summary>
    /// Starts the executor to begin executing scheduled tasks.
    /// </summary>
    void Start();
}