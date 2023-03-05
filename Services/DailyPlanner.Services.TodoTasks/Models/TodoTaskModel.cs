using AutoMapper;
using DailyPlanner.Context.Entities;
using static DailyPlanner.Common.Consts.DateTimeFormats;

namespace DailyPlanner.Services.TodoTasks.Models;

/// <summary>
/// Represents a model for a todotask.
/// </summary>
public class TodoTaskModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EstimatedCompletionTime { get; set; } = string.Empty;
    public string ActualCompletionTime { get; set; } = string.Empty;
    public int NotebookId { get; set; }
    public string NotebookTitle { get; set;} = string.Empty;
}

/// <summary>
/// Maps <see cref="TodoTask"/> to <see cref="TodoTaskModel"/>.
/// </summary>
public class TodoTaskModelProfile : Profile
{
    public TodoTaskModelProfile()
    {
        CreateMap<TodoTask, TodoTaskModel>()
            .ForMember(dest => dest.Status, options => options.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.StartTime,
                options => options.MapFrom(src => src.StartTime.ToString(DATE_TIME_WITHOUT_SECONDS)))
            .ForMember(dest => dest.EstimatedCompletionTime,
                options => options.MapFrom(src => src.EstimatedCompletionTime.ToString(DATE_TIME_WITHOUT_SECONDS)))
            .ForMember(dest => dest.ActualCompletionTime,
                options => options.MapFrom(src => src.ActualCompletionTime == DateTime.MinValue ? null : src.ActualCompletionTime.ToString(DATE_TIME_WITHOUT_SECONDS)))
            .ForMember(dest => dest.NotebookTitle, options => options.MapFrom(src => src.Notebook.Title));
    }
}