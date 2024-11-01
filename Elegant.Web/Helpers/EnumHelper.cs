using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Elegant.Web.Helpers;

public class EnumHelper
{
    public static string GetDisplaName(Enum enumValue)
    {
        return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
    }
}