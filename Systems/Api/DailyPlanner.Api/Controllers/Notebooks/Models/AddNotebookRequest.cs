using AutoMapper;
using DailyPlanner.Services.Notebooks.Models;
using FluentValidation;

namespace DailyPlanner.Api.Controllers.Notebooks.Models;

/// <summary>
/// Represents a request object for a new notebook.
/// </summary>
public class AddNotebookRequest
{
    /// <summary>
    /// Gets or sets the title of the notebook.
    /// </summary>
    public string Title { get; set; } = string.Empty;
}

/// <summary>
/// Validates the <see cref="AddNotebookRequest"/>.
/// </summary>
public class AddNotebookRequestValidator : AbstractValidator<AddNotebookRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddNotebookRequestValidator"/> class.
    /// </summary>
    public AddNotebookRequestValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");
    }
}

/// <summary>
/// Maps <see cref="AddNotebookRequest"/> to <see cref="AddNotebookModel"/>.
/// </summary>
public class AddNotebookRequestProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddNotebookRequestProfile"/> class.
    /// </summary>
    public AddNotebookRequestProfile()
    {
        CreateMap<AddNotebookRequest, AddNotebookModel>();
    }
}