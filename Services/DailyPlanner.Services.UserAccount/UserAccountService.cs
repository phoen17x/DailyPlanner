using AutoMapper;
using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Validator;
using DailyPlanner.Context.Entities.User;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="UserAccountService"/> class.
    /// </summary>
    /// <param name="mapper">Object mapper that maps entities to models and vice versa.</param>
    /// <param name="userManager">User manager.</param>
    /// <param name="registerUserAccountModelValidator">Validator for <see cref="RegisterUserAccountModel"/>.</param>
    public UserAccountService(IMapper mapper, UserManager<User> userManager, IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<UserAccountModel> AddUserAccount(RegisterUserAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is not null)
            throw new ProcessException($"User with email {model.Email} already exists.");

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
            throw new ProcessException($"Cannot create user. {string.Join(" ", result.Errors.Select(s => s.Description))}");

        return mapper.Map<UserAccountModel>(user);
    }
}