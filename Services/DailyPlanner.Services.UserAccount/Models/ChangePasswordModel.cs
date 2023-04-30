using FluentValidation;

namespace DailyPlanner.Services.UserAccount.Models;

/// <summary>
/// Represents the data required for changing a user's password.
/// </summary>
public class ChangePasswordModel
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmationPassword { get; set; }
}

/// <summary>
/// Validates the <see cref="ChangePasswordModel"/>.
/// </summary>
public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidator()
    {
        RuleFor(model => model.Token)
            .NotEmpty().WithMessage("Token is required.");

        RuleFor(model => model.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");

        RuleFor(model => model.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is too long.")
            .MinimumLength(8).WithMessage("Password is too short.");

        RuleFor(model => model.ConfirmationPassword)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is too long.")
            .MinimumLength(8).WithMessage("Password is too short.")
            .Equal(request => request.Password).WithMessage("Passwords must match.");
    }
}