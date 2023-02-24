using AutoMapper;
using DailyPlanner.Services.Notebooks.Models;
using FluentValidation;

namespace DailyPlanner.Api.Controllers.Notebooks.Models;

/// <summary>
/// Represents a request object for an updated notebook.
/// </summary>
public class UpdateNotebookRequest
{
    /// <summary>
    /// Gets or sets the title of the notebook.
    /// </summary>
    public string Title { get; set; } = string.Empty;
}

/// <summary>
/// Validates the <see cref="UpdateNotebookRequest"/>.
/// </summary>
public class UpdateNotebookRequestValidator : AbstractValidator<UpdateNotebookRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateNotebookRequestValidator"/> class.
    /// </summary>
    public UpdateNotebookRequestValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");
    }
}

/// <summary>
/// Maps <see cref="UpdateNotebookRequest"/> to <see cref="UpdateNotebookModel"/>.
/// </summary>
public class UpdateNotebookRequestProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateNotebookRequestProfile"/> class.
    /// </summary>
    public UpdateNotebookRequestProfile()
    {
        CreateMap<UpdateNotebookRequest, UpdateNotebookModel>();
    }
}