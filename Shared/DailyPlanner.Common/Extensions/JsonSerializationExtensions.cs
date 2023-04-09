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

    public static T? FromJsonString<T>(this string obj, JsonSerializerSettings? settings = null)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(obj, settings ?? new JsonSerializerSettings().SetDefaultSettings());
        }
        catch (Exception ex)
        {
            throw new JsonException("Failed to convert to string", ex);
        }
    }

}