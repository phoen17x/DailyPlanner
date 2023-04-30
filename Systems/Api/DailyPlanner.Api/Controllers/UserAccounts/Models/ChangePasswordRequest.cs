using AutoMapper;
using DailyPlanner.Services.UserAccount.Models;
using FluentValidation;

namespace DailyPlanner.Api.Controllers.UserAccounts.Models;

/// <summary>
/// Represents a request object for changing password.
/// </summary>
public class ChangePasswordRequest
{
    /// <summary>
    /// Gets or sets the token for changing password.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the user for whom the password is being changed.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the confirmation password.
    /// </summary>
    public string ConfirmationPassword { get; set; } = string.Empty;
}

/// <summary>
/// Validates the <see cref="ChangePasswordRequest"/>.
/// </summary>
public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordRequestValidator"/> class.
    /// </summary>
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

/// <summary>
/// Maps <see cref="ChangePasswordRequest"/> to <see cref="ChangePasswordModel"/>.
/// </summary>
public class ChangePasswordRequestProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordRequestProfile"/> class.
    /// </summary>
    public ChangePasswordRequestProfile()
    {
        CreateMap<ChangePasswordRequest, ChangePasswordModel>();
    }
}