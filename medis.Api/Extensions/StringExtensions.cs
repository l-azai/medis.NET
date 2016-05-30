using System.Text.RegularExpressions;

namespace medis.Api.Extensions
{
    public static class StringExtensions
    {
        public static string SanitizeString(this string value){
            return Regex.Replace(value, @"[^a-zA-Z0-9\s-]", string.Empty);
        }

        public static string Slugify(this string value) {
            var sanitizedString = SanitizeString(value);

            return sanitizedString.Trim()
                .ToLower()
                .Replace(" ", "-");
        }
    }
}