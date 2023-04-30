using AutoMapper;
using DailyPlanner.Services.UserAccount.Models;
using FluentValidation;

namespace DailyPlanner.Api.Controllers.UserAccounts.Models;

/// <summary>
/// Represents a request object for sending email with link.
/// </summary>
public class SendEmailWithLinkRequest
{
    /// <summary>
    /// Gets or sets the link to frontend page.
    /// </summary>
    public string FrontendUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// Validates the <see cref="SendEmailWithLinkRequest"/>.
/// </summary>
public class SendEmailWithLinkRequestValidator : AbstractValidator<SendEmailWithLinkRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendEmailWithLinkRequestValidator"/> class.
    /// </summary>
    public SendEmailWithLinkRequestValidator()
    {
        RuleFor(model => model.FrontendUrl)
            .NotEmpty().WithMessage("Url is required.");

        RuleFor(model => model.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");
    }
}

/// <summary>
/// Maps <see cref="SendEmailWithLinkRequest"/> to <see cref="SendEmailWithLinkModel"/>.
/// </summary>
public class SendEmailWithLinkRequestProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendEmailWithLinkRequestProfile"/> class.
    /// </summary>
    public SendEmailWithLinkRequestProfile()
    {
        CreateMap<SendEmailWithLinkRequest, SendEmailWithLinkModel>();
    }
}