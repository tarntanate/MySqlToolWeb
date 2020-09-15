using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Ookbee.Ads.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            if (value == default)
            {
                return string.Empty;
            }

            var attribute = value.GetAttribute<DescriptionAttribute>();

            return attribute == default ? value.ToString() : attribute.Description;
        }

        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            if (value == default)
            {
                return default;
            }

            var member = value.GetType().GetMember(value.ToString());

            var attributes = member[0].GetCustomAttributes(typeof(T), false);

            return (T)attributes[0];
        }
    }
}
