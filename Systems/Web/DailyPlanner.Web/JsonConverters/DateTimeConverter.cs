using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DailyPlanner.Web.JsonConverters;

/// <summary>
/// Converts a string representation of a date and time in the specified format to a <see cref="DateTime"/> object during deserialization,
/// and serializes a <see cref="DateTime"/> object to a string representation of a date and time in the specified format during serialization.
/// </summary>
public class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string format;

    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeConverter"/> class.
    /// </summary>
    /// <param name="format">The date and time format string used for serialization and deserialization.</param>
    public DateTimeConverter(string format)
    {
        this.format = format;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var date = reader.GetString();
        return date is null ? DateTime.MinValue : DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(format));
    }
}