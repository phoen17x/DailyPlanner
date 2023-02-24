using Newtonsoft.Json;

namespace DailyPlanner.Common.JsonConverters;

/// <summary>
/// Trims string values.
/// </summary>
public class TrimmingConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(string);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        return reader.Value?.ToString()?.Trim();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is null) writer.WriteNull();
        else writer.WriteValue(value.ToString()?.Trim());
    }
}