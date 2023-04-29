using AutoMapper;
using DailyPlanner.Api.Controllers.Notifications.Models;
using DailyPlanner.Common.Responses;
using DailyPlanner.Common.Security;
using DailyPlanner.Services.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DailyPlanner.Api.Controllers.Notifications;

/// <summary>
/// API endpoints for managing notifications.
/// </summary>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/notifications")]
[Authorize]
[ApiController]
[ApiVersion("1.0")]
public class NotificationController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly INotificationService notificationService;

    /// <summary>
    /// Creates a new instance of the <see cref="NotificationController"/> class.
    /// </summary>
    /// <param name="mapper">The <see cref="IMapper"/> instance used for mapping.</param>
    /// <param name="notificationService">The <see cref="INotificationService"/> instance used for notification operations.</param>
    public NotificationController(IMapper mapper, INotificationService notificationService)
    {
        this.mapper = mapper;
        this.notificationService = notificationService;
    }

    [HttpGet]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(IEnumerable<NotificationResponse>), 200)]
    public async Task<IEnumerable<NotificationResponse>> GetNotifications()
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        var notifications = await notificationService.GetNotifications(userId);
        return mapper.Map<IEnumerable<NotificationResponse>>(notifications);
    }

    [HttpPut("mark-as-read/{notificationId}")]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> MarkAsRead([FromRoute] int notificationId)
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        await notificationService.MarkAsRead(userId, notificationId);
        return Ok();
    }

    [HttpPut("mark-as-read")]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> MarkAllAsRead([FromRoute] int notificationId)
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        await notificationService.MarkAllAsRead(userId);
        return Ok();
    }

    [HttpDelete("{notificationId}")]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> DeleteNotification([FromRoute] int notificationId)
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        await notificationService.DeleteNotification(userId, notificationId);
        return Ok();
    }

    [HttpDelete]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> DeleteAllNotifications([FromRoute] int notificationId)
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        await notificationService.DeleteAllNotifications(userId);
        return Ok();
    }
}