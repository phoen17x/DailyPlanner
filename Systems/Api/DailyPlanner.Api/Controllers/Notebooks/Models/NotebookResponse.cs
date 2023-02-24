using AutoMapper;
using DailyPlanner.Services.Notebooks.Models;

namespace DailyPlanner.Api.Controllers.Notebooks.Models;

/// <summary>
/// Represents a response object for a notebook.
/// </summary>
public class NotebookResponse
{
    /// <summary>
    /// Gets or sets the ID of the notebook.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the notebook.
    /// </summary>
    public string Title { get; set; } = string.Empty;
}

/// <summary>
/// Maps <see cref="NotebookModel"/> to <see cref="NotebookResponse"/>.
/// </summary>
public class NotebookResponseProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotebookResponseProfile"/> class.
    /// </summary>
    public NotebookResponseProfile()
    {
        CreateMap<NotebookModel, NotebookResponse>();
    }
}