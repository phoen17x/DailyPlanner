using AutoMapper;
using DailyPlanner.Api.Controllers.UserAccounts.Models;
using DailyPlanner.Common.Responses;
using DailyPlanner.Common.Security;
using DailyPlanner.Services.UserAccount;
using DailyPlanner.Services.UserAccount.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DailyPlanner.Api.Controllers.UserAccounts;

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

    /// <summary>
    /// Confirms the email address for a user account.
    /// </summary>
    /// <param name="request">The <see cref="ConfirmEmailRequest"/> object containing the confirmation details.</param>
    /// <returns>An <see cref="IActionResult"/> object.</returns>
    [HttpPost("confirm-email")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        await userAccountService.ConfirmEmail(request.Token, request.Email);
        return Ok();
    }

    /// <summary>
    /// Sends an email confirmation link to a user.
    /// </summary>
    /// <param name="request">The <see cref="SendEmailWithLinkRequest"/> object containing the email details.</param>
    /// <returns>An <see cref="IActionResult"/> object.</returns>
    [HttpPost("confirmation-email")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> SendEmailConfirmationLink([FromBody] SendEmailWithLinkRequest request)
    {
        await userAccountService.SendConfirmationLinkAsync(mapper.Map<SendEmailWithLinkModel>(request));
        return Ok();
    }

    /// <summary>
    /// Gets a password recovery token for a user.
    /// </summary>
    /// <returns>A string containing the password recovery token.</returns>
    [HttpGet("password-recovery-token")]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<string> GetPasswordRecoveryToken()
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        return await userAccountService.GetPasswordRecoveryToken(userId);
    }

    /// <summary>
    /// Sends a password recovery link to a user.
    /// </summary>
    /// <param name="request">The <see cref="SendEmailWithLinkRequest"/> object containing the email details.</param>
    /// <returns>An <see cref="IActionResult"/> object.</returns>
    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> SendPasswordRecoveryLink([FromBody] SendEmailWithLinkRequest request)
    {
        await userAccountService.SendPasswordRecoveryLinkAsync(mapper.Map<SendEmailWithLinkModel>(request));
        return Ok();
    }

    /// <summary>
    /// Changes the password for a user.
    /// </summary>
    /// <param name="request">The <see cref="ChangePasswordRequest"/> object containing the new password details.</param>
    /// <returns>An <see cref="IActionResult"/> object.</returns>
    [HttpPost("change-password")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        await userAccountService.ChangePassword(mapper.Map<ChangePasswordModel>(request));
        return Ok();
    }

    /// <summary>
    /// Gets the user data for the authenticated user.
    /// </summary>
    /// <returns>A <see cref="UserAccountResponse"/> object.</returns>
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