using FluentValidation;

namespace DailyPlanner.Web.Pages.TodoTasks.Models;

public class TodoTaskModel
{
    public int? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EstimatedCompletionTime { get; set; }
    public DateTime ActualCompletionTime { get; set; }
    public TodoTaskStatus Status { get; set; }
    public int NotebookId { get; set; }
}

public class NotebookModelValidator : AbstractValidator<TodoTaskModel>
{
    public NotebookModelValidator()
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

        RuleFor(model => model.NotebookId)
            .NotEmpty().WithMessage("Notebook is required.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TodoTaskModel>.CreateWithOptions((TodoTaskModel)model, options => options.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}