using FluentValidation;

namespace DailyPlanner.Api.Controllers.UserAccounts.Models;

/// <summary>
/// Represents a request object for confirming email.
/// </summary>
public class ConfirmEmailRequest
{
    /// <summary>
    /// Gets or sets the token for confirming email.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the user for whom the email is being confirmed.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// Validates the <see cref="ConfirmEmailRequest"/>.
/// </summary>
public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmEmailRequestValidator"/> class.
    /// </summary>
    public ConfirmEmailRequestValidator()
    {
        RuleFor(model => model.Token)
            .NotEmpty().WithMessage("Token is required.");

        RuleFor(model => model.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");
    }
}