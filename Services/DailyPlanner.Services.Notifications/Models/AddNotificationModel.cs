using AutoMapper;
using DailyPlanner.Common.Extensions;
using DailyPlanner.Context.Entities;
using FluentValidation;
using static DailyPlanner.Common.Consts.DateTimeFormats;

namespace DailyPlanner.Services.Notifications.Models;

/// <summary>
/// Represents the model used to add a new todotask.
/// </summary>
public class AddNotificationModel
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SendingTime { get; set; } = string.Empty;
    public bool IsMarkedAsRead { get; set; }
}

/// <summary>
/// Validates the <see cref="AddNotificationModel"/>.
/// </summary>
public class AddNotificationModelValidator : AbstractValidator<AddNotificationModel>
{
    public AddNotificationModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");

        RuleFor(model => model.Description)
            .MaximumLength(200).WithMessage("Description is too long.");

        RuleFor(model => model.SendingTime)
            .NotEmpty().WithMessage("Sending time is required.");
    }
}

/// <summary>
/// Maps <see cref="AddNotificationModel"/> to <see cref="Notification"/>.
/// </summary>
public class AddNotificationModelProfile : Profile
{
    public AddNotificationModelProfile()
    {
        CreateMap<AddNotificationModel, Notification>()
            .ForMember(dest => dest.SendingTime,
                options => options.MapFrom(src => DateTime.ParseExact(src.SendingTime, DATE_TIME_WITHOUT_SECONDS,
                    System.Globalization.CultureInfo.InvariantCulture).SetKindUtc()));
    }
}