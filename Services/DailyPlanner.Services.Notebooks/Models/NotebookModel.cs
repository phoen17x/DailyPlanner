using AutoMapper;
using DailyPlanner.Context.Entities;

namespace DailyPlanner.Services.Notebooks.Models;

/// <summary>
/// Represents a model for a notebook.
/// </summary>
public class NotebookModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}

/// <summary>
/// Maps <see cref="Notebook"/> to <see cref="NotebookModel"/>.
/// </summary>
public class NotebookModelProfile : Profile
{
    public NotebookModelProfile()
    {
        CreateMap<Notebook, NotebookModel>();
    }
}