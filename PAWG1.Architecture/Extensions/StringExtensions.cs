
namespace PAWG1.Architecture.Extensions;

    public static class StringExtensions
    {
    public static string AddSalt(this string value)
    {
        return value.EndsWith("==")
            ?value.Replace("==", "AQB001")
            : value;
    }
    }

