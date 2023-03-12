using Microsoft.AspNetCore.Identity;

namespace DailyPlanner.Context.Entities.User;

/// <summary>
/// Represents a User entity.
/// </summary>
public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public UserStatus Status { get; set; }
}

/// <summary>
/// Represents the status of a User.
/// </summary>
public enum UserStatus
{
    Active,
    Blocked
}