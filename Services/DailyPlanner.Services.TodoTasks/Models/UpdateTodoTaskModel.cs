using AutoMapper;
using DailyPlanner.Common.Extensions;
using DailyPlanner.Context.Entities;
using FluentValidation;
using static DailyPlanner.Common.Consts.DateTimeFormats;

namespace DailyPlanner.Services.TodoTasks.Models;

/// <summary>
/// Represents the model used to update the todotask.
/// </summary>
public class UpdateTodoTaskModel
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EstimatedCompletionTime { get; set; } = string.Empty;
    public string ActualCompletionTime { get; set; } = string.Empty;
    public int Status { get; set; }
    public int NotebookId { get; set; }
}

/// <summary>
/// Validates the <see cref="UpdateTodoTaskModel"/>.
/// </summary>
public class UpdateTodoTaskModelValidator : AbstractValidator<UpdateTodoTaskModel>
{
    public UpdateTodoTaskModelValidator()
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

        RuleFor(model => model.ActualCompletionTime)
            .NotEmpty().WithMessage("Actual completion time is required.");

        RuleFor(model => model.NotebookId)
            .NotEmpty().WithMessage("Notebook is required.");
    }
}

/// <summary>
/// Maps <see cref="UpdateTodoTaskModel"/> to <see cref="TodoTask"/>.
/// </summary>
public class UpdateTodoTaskModelProfile : Profile
{
    public UpdateTodoTaskModelProfile()
    {
        CreateMap<UpdateTodoTaskModel, TodoTask>()
            .ForMember(dest => dest.StartTime,
                options => options.MapFrom(src => DateTime.ParseExact(src.StartTime, DATE_TIME_WITHOUT_SECONDS,
                    System.Globalization.CultureInfo.InvariantCulture).SetKindUtc()))
            .ForMember(dest => dest.EstimatedCompletionTime,
                options => options.MapFrom(src => DateTime.ParseExact(src.EstimatedCompletionTime,
                    DATE_TIME_WITHOUT_SECONDS,
                    System.Globalization.CultureInfo.InvariantCulture).SetKindUtc()))
            .ForMember(dest => dest.ActualCompletionTime,
                options => options.MapFrom(src => DateTime.ParseExact(src.ActualCompletionTime, 
                    DATE_TIME_WITHOUT_SECONDS, 
                    System.Globalization.CultureInfo.InvariantCulture).SetKindUtc()))
            .ForMember(dest => dest.Status, options => options.MapFrom(src => src.Status.ToEnum<TodoTaskStatus>()));
    }
}