using DailyPlanner.Common.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace DailyPlanner.Common.Extensions;

/// <summary>
/// Provides methods for configuring JSON serialization.
/// </summary>
public static class JsonSerializationExtensions
{
    /// <summary>
    /// Sets default JSON serialization settings, such as a camel-case property names contract resolver,
    /// string trimming converter, and string enum converter.
    /// </summary>
    /// <param name="settings">The JsonSerializerSettings to set default settings on.</param>
    /// <returns>The updated <see cref="JsonSerializerSettings"/>.</returns>
    public static JsonSerializerSettings SetDefaultSettings(this JsonSerializerSettings settings)
    {
        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        settings.Converters.Add(new TrimmingConverter());
        settings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));

        return settings;
    }

    /// <summary>
    /// Converts an object to a JSON string using the specified settings.
    /// </summary>
    /// <param name="obj">The object to convert to JSON.</param>
    /// <param name="settings">The JsonSerializerSettings to use for the conversion.</param>
    /// <returns>The JSON string representation of the object.</returns>
    public static string ToJsonString(this object obj, JsonSerializerSettings? settings = null)
    {
        try
        {
            return JsonConvert.SerializeObject(obj, settings ?? new JsonSerializerSettings().SetDefaultSettings());
        }
        catch (Exception ex)
        {
            throw new JsonException("Failed to convert to json string", ex);
        }
    }

    /// <summary>
    /// Converts a JSON string to an object of the specified type using the specified settings.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize from JSON.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="settings">The JsonSerializerSettings to use for the deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="T"/>.</returns>
    public static T? FromJsonString<T>(this string json, JsonSerializerSettings? settings = null)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json, settings ?? new JsonSerializerSettings().SetDefaultSettings());
        }
        catch (Exception ex)
        {
            throw new JsonException("Failed to convert.", ex);
        }
    }

}