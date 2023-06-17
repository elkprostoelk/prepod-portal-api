using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PrepodPortal.Common.Extensions;

public static class EnumExtensions
{
    public static string? GetDisplayAttribute(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

        if (displayAttribute != null)
            return displayAttribute.Name;

        return value.ToString();
    }
}