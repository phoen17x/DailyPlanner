using AutoMapper;
using DailyPlanner.Services.UserAccount.Models;
using FluentValidation;

namespace DailyPlanner.Api.Controllers.UserAccounts.Models;

public class ChangePasswordRequest
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmationPassword { get; set; }
}

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
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

public class ChangePasswordRequestProfile : Profile
{
    public ChangePasswordRequestProfile()
    {
        CreateMap<ChangePasswordRequest, ChangePasswordModel>();
    }
}