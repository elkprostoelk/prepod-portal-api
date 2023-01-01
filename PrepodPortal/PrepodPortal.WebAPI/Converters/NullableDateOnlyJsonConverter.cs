using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PrepodPortal.WebAPI.Converters;

public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
{
    public override DateOnly? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TryGetDateTime(out var dateTime))
        {
            return DateOnly.FromDateTime(dateTime);
        };
        var value = reader.GetString();
        if (value is null)
        {
            return default;
        }
        var match = new Regex("^(\\d\\d\\d\\d)-(\\d\\d)-(\\d\\d)(T|\\s|\\z)")
            .Match(value);
        return match.Success
            ? new DateOnly(int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value))
            : default;
    }
    
    public override void Write(
        Utf8JsonWriter writer,
        DateOnly? value,
        JsonSerializerOptions options) =>
        writer.WriteStringValue(value?.ToString("yyyy-MM-dd"));
}