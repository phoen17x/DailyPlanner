namespace DailyPlanner.Common.Extensions;

/// <summary>
/// Contains extension methods for enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Converts an integer value to an enum of type T.
    /// </summary>
    /// <typeparam name="T">The enum type to convert to.</typeparam>
    /// <param name="value">The integer value to convert.</param>
    /// <returns>The enum value of type T that corresponds to the integer value.</returns>
    /// <exception cref="ArgumentException">Thrown when the integer value is not a valid value for the enum type T.</exception>
    public static T ToEnum<T>(this int value) where T : Enum
    {
        if (Enum.IsDefined(typeof(T), value)) return (T)Enum.ToObject(typeof(T), value);

        throw new ArgumentException($"{value} is not a valid value for enum type {typeof(T).Name}");
    }
}