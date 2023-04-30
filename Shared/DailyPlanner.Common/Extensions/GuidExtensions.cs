namespace DailyPlanner.Common.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="Guid"/>.
/// </summary>
public static class GuidExtensions
{
    /// <summary>
    /// Converts the <see cref="Guid"/> to a string and removes any dashes or spaces, effectively "shrinking" the Guid.
    /// </summary>
    /// <param name="id">The Guid to be "shrunk".</param>
    /// <returns>A string representation of the "shrunk" Guid.</returns>
    public static string Shrink(this Guid id)
    {
        return id.ToString().Replace("-", "").Replace(" ", "");
    }
}