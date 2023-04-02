using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DailyPlanner.Web.JsonConverters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string format;

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