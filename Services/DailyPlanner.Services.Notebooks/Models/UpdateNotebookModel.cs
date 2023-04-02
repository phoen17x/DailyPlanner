using AutoMapper;
using DailyPlanner.Context.Entities;
using FluentValidation;

namespace DailyPlanner.Services.Notebooks.Models;

/// <summary>
/// Represents the model used to update the notebook.
/// </summary>
public class UpdateNotebookModel
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
}

/// <summary>
/// Validates the <see cref="UpdateNotebookModel"/>.
/// </summary>
public class UpdateNotebookModelValidator : AbstractValidator<UpdateNotebookModel>
{
    public UpdateNotebookModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");
    }
}

/// <summary>
/// Maps <see cref="UpdateNotebookModel"/> to <see cref="Notebook"/>.
/// </summary>
public class UpdateNotebookModelProfile : Profile
{
    public UpdateNotebookModelProfile()
    {
        CreateMap<UpdateNotebookModel, Notebook>();
    }
}