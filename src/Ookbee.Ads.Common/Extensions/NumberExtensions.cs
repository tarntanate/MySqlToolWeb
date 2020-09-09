using System;

namespace Ookbee.Ads.Common.Extensions
{
    public static class NumberExtensions
    {
        public static T ToEnum<T>(this int value, bool ignoreCase = true, T defaultValue = default(T)) where T : struct
        {
            if (!typeof(T).IsEnum || !Enum.IsDefined(typeof(T), value) || value < 0)
                return defaultValue;
            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}
