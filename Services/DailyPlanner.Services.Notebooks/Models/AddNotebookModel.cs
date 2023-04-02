using AutoMapper;
using DailyPlanner.Context.Entities;
using FluentValidation;

namespace DailyPlanner.Services.Notebooks.Models;

/// <summary>
/// Represents the model used to add a new notebook.
/// </summary>
public class AddNotebookModel
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
}

/// <summary>
/// Validates the <see cref="AddNotebookModel"/>.
/// </summary>
public class AddNotebookModelValidator : AbstractValidator<AddNotebookModel>
{
    public AddNotebookModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");
    }
}

/// <summary>
/// Maps <see cref="AddNotebookModel"/> to <see cref="Notebook"/>.
/// </summary>
public class AddNotebookModelProfile : Profile
{
    public AddNotebookModelProfile()
    {
        CreateMap<AddNotebookModel, Notebook>();
    }
}