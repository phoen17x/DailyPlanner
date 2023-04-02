using AutoMapper;
using DailyPlanner.Common.Extensions;
using DailyPlanner.Context.Entities;
using FluentValidation;
using static DailyPlanner.Common.Consts.DateTimeFormats;

namespace DailyPlanner.Services.TodoTasks.Models;

/// <summary>
/// Represents the model used to add a new todotask.
/// </summary>
public class AddTodoTaskModel
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EstimatedCompletionTime { get; set; } = string.Empty;
    public int NotebookId { get; set; }
}

/// <summary>
/// Validates the <see cref="AddTodoTaskModel"/>.
/// </summary>
public class AddTodoTaskModelValidator : AbstractValidator<AddTodoTaskModel>
{
    public AddTodoTaskModelValidator()
    {
        RuleFor(model =>  model.Title)
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
/// Maps <see cref="AddTodoTaskModel"/> to <see cref="TodoTask"/>.
/// </summary>
public class AddTodoTaskModelProfile : Profile
{
    public AddTodoTaskModelProfile()
    {
        CreateMap<AddTodoTaskModel, TodoTask>()
            .ForMember(dest => dest.StartTime,
                options => options.MapFrom(src => DateTime.ParseExact(src.StartTime, DATE_TIME_WITHOUT_SECONDS,
                    System.Globalization.CultureInfo.InvariantCulture).SetKindUtc()))
            .ForMember(dest => dest.EstimatedCompletionTime,
                options => options.MapFrom(src => DateTime.ParseExact(src.EstimatedCompletionTime, DATE_TIME_WITHOUT_SECONDS,
                    System.Globalization.CultureInfo.InvariantCulture).SetKindUtc()));
    }
}