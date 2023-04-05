using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PrepodPortal.Common.Extensions;

public static class EnumExtensions
{
    public static string? GetDisplayAttribute(this Enum value) =>
        value.GetType().GetMember(value.ToString())
            .First().GetCustomAttribute<DisplayAttribute>()?.GetName();
}