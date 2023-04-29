using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Api.Controllers.UserAccounts.Models;

public class ConfirmEmailRequest
{
    public string Token { get; set; }
    public string Email { get; set; }
}

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(model => model.Token)
            .NotEmpty().WithMessage("Token is required.");

        RuleFor(model => model.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");
    }
}