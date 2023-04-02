using AutoMapper;
using DailyPlanner.Api.Controllers.UserAccounts.Models;
using DailyPlanner.Common.Responses;
using DailyPlanner.Services.UserAccount;
using DailyPlanner.Services.UserAccount.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Api.Controllers.Accounts;

/// <summary>
/// API endpoints for managing user accounts.
/// </summary>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/accounts")]
[ApiController]
[ApiVersion("1.0")]
public class UserAccountController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IUserAccountService userAccountService;

    /// <summary>
    /// Creates a new instance of the <see cref="UserAccountController"/> class.
    /// </summary>
    /// <param name="mapper">The <see cref="IMapper"/> instance used for mapping.</param>
    /// <param name="userAccountService">The <see cref="IUserAccountService"/> instance used for user account operations.</param>
    public UserAccountController(IMapper mapper, IUserAccountService userAccountService)
    {
        this.mapper = mapper;
        this.userAccountService = userAccountService;
    }

    /// <summary>
    /// Adds a new user account.
    /// </summary>
    /// <param name="request">The <see cref="RegisterUserAccountRequest"/> object containing the account details.</param>
    /// <returns>A <see cref="UserAccountResponse"/> object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserAccountResponse), 200)]
    public async Task<UserAccountResponse> AddUserAccount([FromBody] RegisterUserAccountRequest request)
    {
        var user = await userAccountService.AddUserAccount(mapper.Map<RegisterUserAccountModel>(request));
        var response = mapper.Map<UserAccountResponse>(user);
        return response;
    }
}