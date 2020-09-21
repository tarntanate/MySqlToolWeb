using System;
using System.Collections.Generic;
using System.Linq;

namespace Ookbee.Ads.Common.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<T> GetValues<T>() where T : struct
        {
            var enumType = typeof(T);

            if (enumType.IsEnum)
                return Enum.GetValues(enumType).Cast<T>();

            throw new InvalidOperationException(string.Format($"Type {enumType.FullName} is not enum."));
        }

        public static T ConvertTo<T>(string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
