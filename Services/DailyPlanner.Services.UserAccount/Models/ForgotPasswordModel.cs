using FluentValidation;

namespace DailyPlanner.Services.UserAccount.Models;

public class SendEmailWithLinkModel
{
    public string FrontendUrl { get; set; }
    public string Email { get; set; }
}

public class SendEmailWithLinkValidator : AbstractValidator<SendEmailWithLinkModel>
{
    public SendEmailWithLinkValidator()
    {
        RuleFor(model => model.FrontendUrl)
            .NotEmpty().WithMessage("Url is required.");

        RuleFor(model => model.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");
    }
}