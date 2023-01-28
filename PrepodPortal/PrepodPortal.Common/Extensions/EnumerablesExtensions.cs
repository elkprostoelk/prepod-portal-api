namespace PrepodPortal.Common.Extensions;

public static class EnumerablesExtensions
{
    public static bool NullableSequenceEqual<T>(this IEnumerable<T>? enumerable1, IEnumerable<T>? enumerable2)
    {
        if (enumerable1 is not null && enumerable2 is not null)
        {
            return enumerable1.SequenceEqual(enumerable2);
        }

        return enumerable1 is null && enumerable2 is null;
    }
}