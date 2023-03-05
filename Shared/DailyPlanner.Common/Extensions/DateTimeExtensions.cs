namespace DailyPlanner.Common.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="DateTime"/>.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Sets the kind of the specified <see cref="DateTime"/> instance to UTC if it is not already set to UTC.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> instance to modify.</param>
    /// <returns>A <see cref="DateTime"/> instance with the UTC kind.</returns>
    public static DateTime SetKindUtc(this DateTime dateTime)
    {
        return dateTime.Kind == DateTimeKind.Utc ? dateTime : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}