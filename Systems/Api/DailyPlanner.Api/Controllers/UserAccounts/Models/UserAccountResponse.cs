using AutoMapper;
using DailyPlanner.Services.UserAccount.Models;

namespace DailyPlanner.Api.Controllers.UserAccounts.Models;

/// <summary>
/// Represents a response object for a user.
/// </summary>
public class UserAccountResponse
{
    /// <summary>
    /// Gets or sets the id of the user.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the user email.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// Maps <see cref="UserAccountModel"/> to <see cref="UserAccountResponse"/>.
/// </summary>
public class UserAccountResponseProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserAccountResponseProfile"/> class.
    /// </summary>
    public UserAccountResponseProfile()
    {
        CreateMap<UserAccountModel, UserAccountResponse>();
    }
}