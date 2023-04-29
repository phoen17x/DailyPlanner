using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace DailyPlanner.Services.Notifications;

public class NotificationHub : Hub
{
    public static ConcurrentDictionary<string, string> Connections = new();

    public override async Task OnConnectedAsync()
    {
        string? userId = Context.GetHttpContext()?.Request.Query["user-id"];
        if (userId is null) throw new Exception("User ID is null.");

        if (Connections.ContainsKey(userId) == false)
            Connections.TryAdd(userId, Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string? userId = Context.GetHttpContext()?.Request.Query["user-id"];
        if (userId is null) throw new Exception("User ID is null.");

        Connections.TryRemove(userId, out _);
    }
}