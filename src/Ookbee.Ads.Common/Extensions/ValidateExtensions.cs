
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

        public static bool IsValidJpeg(this string value)
        {
            var regex = new Regex("^.(jpg|jpeg)$");
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
            return Uri.TryCreate(value, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
