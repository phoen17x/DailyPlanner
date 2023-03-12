using Microsoft.AspNetCore.Identity;

namespace DailyPlanner.Context.Entities.User;

/// <summary>
/// Represents a role in the identity system.
/// </summary>
public class UserRole : IdentityRole<Guid>
{

}