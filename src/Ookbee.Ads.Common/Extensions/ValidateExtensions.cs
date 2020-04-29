
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

        public static bool IsValidPhoneNumber(this string value)
        {
            var regex = new Regex(@"^(02)[0-9]\d{6}$|^(06|08|09)[0-9]\d{7}$");
            return regex.IsMatch(value);
        }
    }
}
