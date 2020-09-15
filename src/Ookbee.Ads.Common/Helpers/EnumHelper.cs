using System;
using System.Collections.Generic;
using System.Linq;

namespace Ookbee.Ads.Common.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<T> GetValues<T>() where T : struct, IComparable, IFormattable, IConvertible
        {
            var enumType = typeof(T);

            if (enumType.IsEnum)
                return Enum.GetValues(enumType).Cast<T>();

            throw new InvalidOperationException(string.Format($"Type {enumType.FullName} is not enum."));
        }
    }
}
