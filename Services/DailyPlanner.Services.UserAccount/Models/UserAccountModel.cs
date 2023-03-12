using AutoMapper;
using DailyPlanner.Context.Entities.User;

namespace DailyPlanner.Services.UserAccount.Models;

/// <summary>
/// Represents a model for a user.
/// </summary>
public class UserAccountModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

/// <summary>
/// Maps <see cref="User"/> to <see cref="UserAccountModel"/>.
/// </summary>
public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<User, UserAccountModel>();
    }
}