using Microsoft.AspNetCore.Identity;

namespace DailyPlanner.Context.Entities.User;

/// <summary>
/// Represents a User entity.
/// </summary>
public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public UserStatus Status { get; set; }
    public ICollection<Notebook> Notebooks { get; set; }
    public ICollection<TodoTask> TodoTasks { get; set; }
    public ICollection<Notification> Notifications { get; set; }
}

/// <summary>
/// Represents the status of a User.
/// </summary>
public enum UserStatus
{
    Active,
    Blocked
}