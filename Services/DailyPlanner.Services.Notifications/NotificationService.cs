using AutoMapper;
using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Validator;
using DailyPlanner.Context;
using DailyPlanner.Context.Entities;
using DailyPlanner.Services.Notifications.Models;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Common.Extensions;
using static DailyPlanner.Common.Consts.DateTimeFormats;
using Microsoft.AspNetCore.SignalR;

namespace DailyPlanner.Services.Notifications;

public class NotificationService : INotificationService
{
    private readonly IDbContextFactory<DailyPlannerContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddNotificationModel> addNotificationModelValidator;
    private readonly IHubContext<NotificationHub> hubContext;


    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationService"/> class.
    /// </summary>
    /// <param name="contextFactory">Factory that provides access to the <see cref="DailyPlannerContext"/>.</param>
    /// <param name="mapper">Object mapper that maps entities to models and vice versa.</param>
    /// <param name="addNotificationModelValidator">Validator for <see cref="AddNotificationModel"/>.</param>
    /// <param name="hubContext">The hub context for the <see cref="NotificationHub"/>.</param>
    public NotificationService(
        IDbContextFactory<DailyPlannerContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddNotificationModel> addNotificationModelValidator,
        IHubContext<NotificationHub> hubContext)
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.addNotificationModelValidator = addNotificationModelValidator;
        this.hubContext = hubContext;
    }

    public async Task<IEnumerable<NotificationModel>> GetNotifications(Guid userId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var notifications = context.Notifications.Where(notification => notification.UserId == userId);

        var data = (await notifications.ToListAsync()).Select(mapper.Map<NotificationModel>);
        return data;
    }

    public async Task<NotificationModel> AddNotification(AddNotificationModel model)
    {
        addNotificationModelValidator.Check(model);

        await using var context = await contextFactory.CreateDbContextAsync();
        var notification = mapper.Map<Notification>(model);

        await context.Notifications.AddAsync(notification);
        await context.SaveChangesAsync();

        return mapper.Map<NotificationModel>(notification);
    }

    public async Task AddTodoTaskReminders()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var now = DateTime.Now.SetKindUtc();
        var tasks = context.TodoTasks
            .Where(task => task.IsReminderSent == false &&
                           task.Status == TodoTaskStatus.Scheduled &&
                           task.StartTime > now && 
                           task.StartTime <= now.AddHours(1));

        foreach (var task in tasks)
        {
            var notification = new AddNotificationModel()
            {
                Title = task.Title,
                Description = $"Task starts at {task.StartTime:h:mm tt}.",
                SendingTime = DateTime.Now.ToString(DATE_TIME_WITHOUT_SECONDS),
                UserId = task.UserId
            };

            await context.Notifications.AddAsync(mapper.Map<Notification>(notification));
            task.IsReminderSent = true;
            context.TodoTasks.Update(task);

            NotificationHub.Connections.TryGetValue(task.UserId.ToString(), out var connectionId);
            await hubContext.Clients.Client(connectionId ?? "").SendAsync("ReceiveNotification", $"\"{task.Title}\" starts at {task.StartTime:h:mm tt}.");
        }

        await context.SaveChangesAsync();
    }

    public async Task MarkAsRead(Guid userId, int notificationId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var notification = await context.Notifications
            .Where(notification => notification.UserId == userId)
            .FirstOrDefaultAsync(notification => notification.Id == notificationId);
        ProcessException.ThrowIfNull(notification, $"The notification with id {notificationId} was not found.");

        if (notification!.IsMarkedAsRead) return;

        notification.IsMarkedAsRead = true;
        context.Notifications.Update(notification);
        await context.SaveChangesAsync();
    }

    public async Task MarkAllAsRead(Guid userId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var notifications = context.Notifications.Where(notification => notification.UserId == userId);

        foreach (var notification in notifications)
        {
            if (notification.IsMarkedAsRead) continue;

            notification.IsMarkedAsRead = true;
            context.Notifications.Update(notification);
        }

        await context.SaveChangesAsync();
    }

    public async Task DeleteNotification(Guid userId, int notificationId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var notification = await context.Notifications
            .Where(notification => notification.UserId == userId)
            .FirstOrDefaultAsync(notification => notification.Id == notificationId);
        ProcessException.ThrowIfNull(notification, $"The notification with id {notificationId} was not found.");

        context.Remove(notification!);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAllNotifications(Guid userId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var notifications = context.Notifications.Where(notification => notification.UserId == userId);

        foreach (var notification in notifications)
            context.Remove(notification);

        await context.SaveChangesAsync();
    }
}