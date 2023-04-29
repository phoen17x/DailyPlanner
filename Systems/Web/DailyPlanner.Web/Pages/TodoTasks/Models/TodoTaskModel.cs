using FluentValidation;

namespace DailyPlanner.Web.Pages.TodoTasks.Models;

public class TodoTaskModel
{
    public int? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EstimatedCompletionTime { get; set; }
    public DateTime? NullableStartTime { get; set; } = DateTime.Now; // for form validation
    public DateTime? NullableEstimatedCompletionTime { get; set; } = DateTime.Now.AddHours(1); // for form validation
    public DateTime ActualCompletionTime { get; set; }
    public TodoTaskStatus Status { get; set; }
    public int NotebookId { get; set; }

    public TodoTaskModel() {}

    public TodoTaskModel(TodoTask todotask)
    {
        Id = todotask.Id;
        Title = todotask.Title;
        Description = todotask.Description;
        StartTime = todotask.StartTime;
        NullableStartTime = todotask.StartTime;
        EstimatedCompletionTime = todotask.EstimatedCompletionTime;
        NullableEstimatedCompletionTime = todotask.EstimatedCompletionTime;
        ActualCompletionTime = todotask.ActualCompletionTime;
        Status = todotask.Status;
        NotebookId = todotask.NotebookId;
    }
}

public class TodoTaskModelValidator : AbstractValidator<TodoTaskModel>
{
    public TodoTaskModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");

        RuleFor(model => model.Description)
            .MaximumLength(200).WithMessage("Description is too long.");

        RuleFor(model => model.StartTime)
            .NotEmpty().WithMessage("Start time is required.");

        RuleFor(model => model.EstimatedCompletionTime)
            .NotEmpty().WithMessage("Estimated completion time is required.")
            .GreaterThan(model => model.StartTime).WithMessage("Estimated completion time should be greater than start time.");

        RuleFor(model => model.NullableStartTime!.Value)
            .NotEmpty().WithMessage("Start time is required.");

        RuleFor(model => model.NullableEstimatedCompletionTime!.Value)
            .NotEmpty().WithMessage("Estimated completion time is required.")
            .GreaterThan(model => model.NullableStartTime!.Value).WithMessage("Estimated completion time should be greater than start time.");

        RuleFor(model => model.NotebookId)
            .NotEmpty().WithMessage("Notebook is required.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TodoTaskModel>.CreateWithOptions((TodoTaskModel)model, options => options.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}