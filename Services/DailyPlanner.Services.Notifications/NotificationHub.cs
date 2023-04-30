using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace DailyPlanner.Services.Notifications;

/// <summary>
/// Represents the SignalR hub for real-time notifications.
/// </summary>
public class NotificationHub : Hub
{
    /// <summary>
    /// The dictionary that holds the connections to the hub.
    /// </summary>
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