using AutoMapper;
using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Responses;
using DailyPlanner.Common.Validator;
using DailyPlanner.Context.Entities.User;
using DailyPlanner.Services.Actions;
using DailyPlanner.Services.EmailSender.Models;
using DailyPlanner.Services.UserAccount.Models;
using Microsoft.AspNetCore.Identity;

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
            Email = model.Email,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
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

        await action.SendEmail(new EmailModel
        {
            Email = model.Email,
            Subject = "Account Creation",
            Message = "Successfully registered!"
        });


        return mapper.Map<UserAccountModel>(user);
    }
}