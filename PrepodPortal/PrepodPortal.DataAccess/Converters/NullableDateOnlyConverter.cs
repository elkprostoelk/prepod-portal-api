using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PrepodPortal.DataAccess.Converters;

public class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>
{
    public NullableDateOnlyConverter() : base(
        dateOnly => !dateOnly.HasValue
            ? null
            : new DateTime?(dateOnly.Value.ToDateTime(TimeOnly.MinValue)),
        dateTime => !dateTime.HasValue
            ? null
            : new DateOnly?(DateOnly.FromDateTime(dateTime.Value)))
    { }
}