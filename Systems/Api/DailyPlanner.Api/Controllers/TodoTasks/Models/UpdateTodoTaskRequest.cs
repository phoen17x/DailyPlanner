using AutoMapper;
using DailyPlanner.Services.TodoTasks.Models;
using FluentValidation;

namespace DailyPlanner.Api.Controllers.TodoTasks.Models;

/// <summary>
/// Represents a request object for an updated todotask.
/// </summary>
public class UpdateTodoTaskRequest
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
    /// Gets or sets the status of the todotask.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the id of the notebook to which the todotask belongs.
    /// </summary>
    public int NotebookId { get; set; }
}

/// <summary>
/// Validates the <see cref="UpdateTodoTaskRequest"/>.
/// </summary>
public class UpdateTodoTaskRequestValidator : AbstractValidator<UpdateTodoTaskRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTodoTaskRequestValidator"/> class.
    /// </summary>
    public UpdateTodoTaskRequestValidator()
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
/// Maps <see cref="UpdateTodoTaskRequest"/> to <see cref="UpdateTodoTaskModel"/>.
/// </summary>
public class UpdateTodoTaskModelProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTodoTaskModelProfile"/> class.
    /// </summary>
    public UpdateTodoTaskModelProfile()
    {
        CreateMap<UpdateTodoTaskRequest, UpdateTodoTaskModel>();
    }
}