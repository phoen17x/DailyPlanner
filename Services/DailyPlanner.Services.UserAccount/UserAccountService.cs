using AutoMapper;
using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Responses;
using DailyPlanner.Common.Validator;
using DailyPlanner.Context.Entities.User;
using DailyPlanner.Services.Actions;
using DailyPlanner.Services.EmailSender.Models;
using DailyPlanner.Services.UserAccount.Models;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Services.UserAccount;

/// <summary>
/// Provides functionality related to user account management.
/// </summary>
public class UserAccountService : IUserAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;
    private readonly IAction action;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserAccountService"/> class.
    /// </summary>
    /// <param name="mapper">Object mapper that maps entities to models and vice versa.</param>
    /// <param name="userManager">User manager.</param>
    /// <param name="registerUserAccountModelValidator">Validator for <see cref="RegisterUserAccountModel"/>.</param>
    /// <param name="action">RabbitMQ actions.</param>
    public UserAccountService(
        IMapper mapper,
        UserManager<User> userManager,
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator,
        IAction action)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
        this.action = action;
    }

    public async Task<UserAccountModel> AddUserAccount(RegisterUserAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is not null)
        {
            var fieldErrors = new List<ErrorResponseFieldInfo>
            {
                new ErrorResponseFieldInfo()
                {
                    FieldName = "email",
                    Message = $"User with email {model.Email} already exists."
                }
            };
            var errorResponse = new ErrorResponse
            {
                Message = "Cannot create user.",
                FieldErrors = fieldErrors
            };
            throw new ProcessException(errorResponse);
        }


        user = new User()
        {
            Status = UserStatus.Active,
            Name = model.Name,
            UserName = model.Email,
            Email = model.Email
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded == false)
        {
            var fieldErrors = result.Errors.Select(error => new ErrorResponseFieldInfo { Message = error.Description }).ToList();
            var errorResponse = new ErrorResponse
            {
                Message = "Cannot create user.",
                FieldErrors = fieldErrors
            };
            throw new ProcessException(errorResponse);
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = string.Format(model.FrontendUrl, token, model.Email);
        await SendConfirmationLinkAsync(model.Email, link);

        return mapper.Map<UserAccountModel>(user);
    }

    public async Task SendConfirmationLinkAsync(SendEmailWithLinkModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            var fieldErrors = new List<ErrorResponseFieldInfo>
            {
                new ErrorResponseFieldInfo()
                {
                    FieldName = "email",
                    Message = $"The user with email {model.Email} was not found."
                }
            };
            var errorResponse = new ErrorResponse
            {
                Message = "Cannot find user.",
                FieldErrors = fieldErrors
            };
            throw new ProcessException(errorResponse);
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = string.Format(model.FrontendUrl, token, model.Email);

        var emailModel = new EmailModel
        {
            Email = model.Email,
            Subject = "Confirm your account",
            Message = $"<a href=\"{confirmationLink}\">Click</a> to confirm email."
        };
        await action.SendEmail(emailModel);
    }

    public async Task SendConfirmationLinkAsync(string email, string confirmationLink)
    {
        var emailModel = new EmailModel
        {
            Email = email,
            Subject = "Confirm your account",
            Message = $"<a href=\"{confirmationLink}\">Click</a> to confirm email."
        };
        await action.SendEmail(emailModel);
    }

    public async Task ConfirmEmail(string token, string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            var fieldErrors = new List<ErrorResponseFieldInfo>
            {
                new ErrorResponseFieldInfo()
                {
                    FieldName = "email",
                    Message = $"The user with email {email} was not found."
                }
            };
            var errorResponse = new ErrorResponse
            {
                Message = "Cannot find user.",
                FieldErrors = fieldErrors
            };
            throw new ProcessException(errorResponse);
        }

        if (user.EmailConfirmed) return;

        var result = await userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded == false)
        {
            var fieldErrors = result.Errors.Select(error => new ErrorResponseFieldInfo { Message = error.Description }).ToList();
            var errorResponse = new ErrorResponse
            {
                Message = "Couldn't change password.",
                FieldErrors = fieldErrors
            };
            throw new ProcessException(errorResponse);
        }
    }

    public async Task<string> GetPasswordRecoveryToken(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        ProcessException.ThrowIfNull(user, "The user was not found.");
        return await userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task SendPasswordRecoveryLinkAsync(SendEmailWithLinkModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            var fieldErrors = new List<ErrorResponseFieldInfo>
            {
                new ErrorResponseFieldInfo()
                {
                    FieldName = "email",
                    Message = $"The user with email {model.Email} was not found."
                }
            };
            var errorResponse = new ErrorResponse
            {
                Message = "Cannot find user.",
                FieldErrors = fieldErrors
            };
            throw new ProcessException(errorResponse);
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var link = string.Format(model.FrontendUrl, token, model.Email);
        var emailModel = new EmailModel
        {
            Email = model.Email,
            Subject = "Password recovery",
            Message = $"<a href=\"{link}\">Click</a> to change password."
        };
        await action.SendEmail(emailModel);
    }

    public async Task ChangePassword(ChangePasswordModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            var fieldErrors = new List<ErrorResponseFieldInfo>
            {
                new ErrorResponseFieldInfo()
                {
                    FieldName = "email",
                    Message = $"The user with email {model.Email} was not found."
                }
            };
            var errorResponse = new ErrorResponse
            {
                Message = "Cannot find user.",
                FieldErrors = fieldErrors
            };
            throw new ProcessException(errorResponse);
        }

        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (result.Succeeded == false)
        {
            var fieldErrors = result.Errors.Select(error => new ErrorResponseFieldInfo { Message = error.Description }).ToList();
            var errorResponse = new ErrorResponse
            {
                Message = "Couldn't change password.",
                FieldErrors = fieldErrors
            };
            throw new ProcessException(errorResponse);
        }
    }

    public async Task<UserAccountModel> GetUserData(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        ProcessException.ThrowIfNull(user, "The user was not found.");
        return mapper.Map<UserAccountModel>(user);
    }
}