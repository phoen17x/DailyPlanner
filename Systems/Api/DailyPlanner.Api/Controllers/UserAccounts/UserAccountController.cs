using AutoMapper;
using DailyPlanner.Api.Controllers.UserAccounts.Models;
using DailyPlanner.Common.Responses;
using DailyPlanner.Common.Security;
using DailyPlanner.Services.UserAccount;
using DailyPlanner.Services.UserAccount.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpPost("confirm-email")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        await userAccountService.ConfirmEmail(request.Token, request.Email);
        return Ok();
    }

    [HttpPost("confirmation-email")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> SendEmailConfirmationLink([FromBody] SendEmailWithLinkRequest request)
    {
        await userAccountService.SendConfirmationLinkAsync(mapper.Map<SendEmailWithLinkModel>(request));
        return Ok();
    }

    [HttpGet("password-recovery-token")]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<string> GetPasswordRecoveryToken()
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        return await userAccountService.GetPasswordRecoveryToken(userId);
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> SendPasswordRecoveryLink([FromBody] SendEmailWithLinkRequest request)
    {
        await userAccountService.SendPasswordRecoveryLinkAsync(mapper.Map<SendEmailWithLinkModel>(request));
        return Ok();
    }

    [HttpPost("change-password")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        await userAccountService.ChangePassword(mapper.Map<ChangePasswordModel>(request));
        return Ok();
    }

    [HttpGet("user")]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(UserAccountResponse), 200)]
    public async Task<UserAccountResponse> GetUserData()
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await userAccountService.GetUserData(userId);
        return mapper.Map<UserAccountResponse>(user);
    }
}