using AutoMapper;
using DailyPlanner.Services.TodoTasks.Models;
using FluentValidation;

namespace DailyPlanner.Api.Controllers.TodoTasks.Models;

/// <summary>
/// Represents a request object for a new todotask.
/// </summary>
public class AddTodoTaskRequest
{
    /// <summary>
    /// Gets or sets the title of the todotask.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the todotask.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start time of the todotask in string format.
    /// </summary>
    public string StartTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated completion time of the todotask in string format.
    /// </summary>
    public string EstimatedCompletionTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the id of the notebook to which the todotask belongs.
    /// </summary>
    public int NotebookId { get; set; }
}

/// <summary>
/// Validates the <see cref="AddTodoTaskRequest"/>.
/// </summary>
public class AddTodoTaskRequestValidator : AbstractValidator<AddTodoTaskRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddTodoTaskRequestValidator"/> class.
    /// </summary>
    public AddTodoTaskRequestValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");

        RuleFor(model => model.Description)
            .MaximumLength(200).WithMessage("Description is too long.");

        RuleFor(model => model.StartTime)
            .NotEmpty().WithMessage("Start time is required.");

        RuleFor(model => model.EstimatedCompletionTime)
            .NotEmpty().WithMessage("Estimated completion time is required.");

        RuleFor(model => model.NotebookId)
            .NotEmpty().WithMessage("Notebook is required.");
    }
}

/// <summary>
/// Maps <see cref="AddTodoTaskRequest"/> to <see cref="AddTodoTaskModel"/>.
/// </summary>
public class AddTodoTaskModelProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddTodoTaskModelProfile"/> class.
    /// </summary>
    public AddTodoTaskModelProfile()
    {
        CreateMap<AddTodoTaskRequest, AddTodoTaskModel>();
    }
}