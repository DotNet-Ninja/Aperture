using System.Text;

namespace Aperture.Extensions;

public static class StringExtensions
{
    public static readonly char[] AllowedSlugChars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-' };
    public static string Slugify(this string value)
    {
        var builder = new StringBuilder();
        var chars = value.ToLower().ToCharArray();
        var lastChar = '-';
        foreach (var current in chars)
        {
            if ((current > 96 && current < 123) || current > 47 && current < 58)
            {
                builder.Append(current);
            }

            if (lastChar != '-' && current is ' ' or '-' or '_')
            {
                builder.Append('-');
            }
        }
        return builder.ToString();
    }
}
