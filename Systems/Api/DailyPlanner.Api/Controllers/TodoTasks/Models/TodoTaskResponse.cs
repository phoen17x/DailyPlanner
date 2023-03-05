using AutoMapper;
using DailyPlanner.Services.TodoTasks.Models;

namespace DailyPlanner.Api.Controllers.TodoTasks.Models;

/// <summary>
/// Represents a response object for a todotask.
/// </summary>
public class TodoTaskResponse
{
    /// <summary>
    /// Gets or sets the id of the todotask.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the todotask.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the todotask.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the todotask.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the start time of the todotask in string format.
    /// </summary>
    public string StartTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated completion time of the todotask in string format.
    /// </summary>
    public string EstimatedCompletionTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the actual completion time of the todotask in string format.
    /// </summary>
    public string ActualCompletionTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the id of the notebook to which the todotask belongs.
    /// </summary>
    public int NotebookId { get; set; }

    /// <summary>
    /// Gets or sets the title of the notebook to which the todotask belongs.
    /// </summary>
    public string NotebookTitle { get; set; } = string.Empty;
}

/// <summary>
/// Maps <see cref="TodoTaskModel"/> to <see cref="TodoTaskResponse"/>.
/// </summary>
public class TodoTaskResponseProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TodoTaskResponseProfile"/> class.
    /// </summary>
    public TodoTaskResponseProfile()
    {
        CreateMap<TodoTaskModel, TodoTaskResponse>();
    }
}