using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Elegant.Common.Helpers;

public static class EnumHelper
{
    public static string? GetDisplayName(Enum enumValue)
    {
        var memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
        return memberInfo == null ? null : memberInfo.GetCustomAttribute<DisplayAttribute>()?.GetName();
    }
}