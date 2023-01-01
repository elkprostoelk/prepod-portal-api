using System.Text.Json;
using PrepodPortal.WebAPI.Converters;

namespace PrepodPortal.WebAPI.Extensions;

public static class DateOnlyJsonConverterExtensions
{
    public static void AddDateOnlyConverters(
        this JsonSerializerOptions options)    
    {
        options.Converters.Add(new DateOnlyJsonConverter());
        options.Converters.Add(new NullableDateOnlyJsonConverter());
    }
}