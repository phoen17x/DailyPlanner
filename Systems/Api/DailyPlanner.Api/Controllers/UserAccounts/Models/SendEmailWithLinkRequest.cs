using AutoMapper;
using DailyPlanner.Services.UserAccount.Models;
using FluentValidation;

namespace DailyPlanner.Api.Controllers.UserAccounts.Models;

public class SendEmailWithLinkRequest
{
    public string FrontendUrl { get; set; }
    public string Email { get; set; }
}

public class SendEmailWithLinkRequestValidator : AbstractValidator<SendEmailWithLinkRequest>
{
    public SendEmailWithLinkRequestValidator()
    {
        RuleFor(model => model.FrontendUrl)
            .NotEmpty().WithMessage("Url is required.");

        RuleFor(model => model.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");
    }
}

public class SendEmailWithLinkRequestProfile : Profile
{
    public SendEmailWithLinkRequestProfile()
    {
        CreateMap<SendEmailWithLinkRequest, SendEmailWithLinkModel>();
    }
}