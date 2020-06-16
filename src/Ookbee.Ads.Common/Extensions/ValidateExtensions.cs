
using System;
using System.Text.RegularExpressions;

namespace Ookbee.Ads.Common.Extensions
{
    public static class ValidateExtensions
    {
        public static bool IsValidEmailAddress(this string value)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(value);
        }

        public static bool IsValidISO8601(this string value)
        {
            var regex = new Regex(@"^(?:[1-9]\d{3}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1\d|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[1-9]\d(?:0[48]|[2468][048]|[13579][26])|(?:[2468][048]|[13579][26])00)-02-29)T(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d(?:\.\d{1,9})?(?:Z|[+-][01]\d:[0-5]\d)$");
            return regex.IsMatch(value.ToLower());
        }

        public static bool IsValidJpeg(this string value)
        {
            var regex = new Regex("^.(jpg|jpeg)$");
            return regex.IsMatch(value.ToLower());
        }

        public static bool IsValidPng(this string value)
        {
            var regex = new Regex("^.(png)$");
            return regex.IsMatch(value.ToLower());
        }

        public static bool IsValidPhoneNumber(this string value)
        {
            var regex = new Regex(@"^(02)[0-9]\d{6}$|^(06|08|09)[0-9]\d{7}$");
            return regex.IsMatch(value);
        }

        public static bool IsValidRGBHexColor(this string value)
        {
            var regex = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$");
            return regex.IsMatch(value.ToLower());
        }

        public static bool IsValidUri(this string value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out var uriResult);
        }

        public static bool IsValidUriSchemeHttp(this string value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
