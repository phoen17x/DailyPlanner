using FluentValidation;

namespace DailyPlanner.Services.UserAccount.Models;

/// <summary>
/// Represents the model used to register a new user.
/// </summary>
public class RegisterUserAccountModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

/// <summary>
/// Validates the <see cref="RegisterUserAccountModel"/>.
/// </summary>
public class RegisterUserAccountModelValidator : AbstractValidator<RegisterUserAccountModel>
{
    public RegisterUserAccountModelValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(16).WithMessage("Username is too long.")
            .MinimumLength(3).WithMessage("Username is too short.");

        RuleFor(model => model.Email)
            .EmailAddress().WithMessage("Email is required.");

        RuleFor(model => model.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is too long.")
            .MinimumLength(8).WithMessage("Password is too short.");
    }
}