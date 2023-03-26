namespace DailyPlanner.Web.Pages.Notebooks.Models;

using FluentValidation;

public class NotebookModel
{
    public int? Id { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class NotebookModelValidator : AbstractValidator<NotebookModel>
{
    public NotebookModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<NotebookModel>.CreateWithOptions((NotebookModel)model, options => options.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}