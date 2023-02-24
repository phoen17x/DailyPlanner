using System.Text.Json;

namespace DailyPlanner.Common.Extensions;

/// <summary>
/// Provides extension methods for strings.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts a string to camel case format, following the JSON naming policy.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The camel case version of the input string.</returns>
    public static string ToCamelCase(this string str)
    {
        return JsonNamingPolicy.CamelCase.ConvertName(str);
    }
}