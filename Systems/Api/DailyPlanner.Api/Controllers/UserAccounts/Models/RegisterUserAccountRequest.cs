﻿using AutoMapper;
using DailyPlanner.Services.UserAccount.Models;
using FluentValidation;

namespace DailyPlanner.Api.Controllers.UserAccounts.Models;

/// <summary>
/// Represents a request object for a new user.
/// </summary>
public class RegisterUserAccountRequest
{
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user email.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user password.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the link to frontend page.
    /// </summary>
    public string FrontendUrl { get; set; } = string.Empty;
}

/// <summary>
/// Validates the <see cref="RegisterUserAccountRequest"/>.
/// </summary>
public class RegisterUserAccountRequestValidator : AbstractValidator<RegisterUserAccountRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserAccountRequestValidator"/> class.
    /// </summary>
    public RegisterUserAccountRequestValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(16).WithMessage("Username is too long.")
            .MinimumLength(3).WithMessage("Username is too short.");

        RuleFor(model => model.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");

        RuleFor(model => model.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is too long.")
            .MinimumLength(8).WithMessage("Password is too short.");

        RuleFor(model => model.FrontendUrl)
            .NotEmpty().WithMessage("Url is required.");
    }
}

/// <summary>
/// Maps <see cref="RegisterUserAccountRequest"/> to <see cref="RegisterUserAccountModel"/>.
/// </summary>
public class RegisterUserAccountRequestProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserAccountRequestProfile"/> class.
    /// </summary>
    public RegisterUserAccountRequestProfile()
    {
        CreateMap<RegisterUserAccountRequest, RegisterUserAccountModel>();
    }
}