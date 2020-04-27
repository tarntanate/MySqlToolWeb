using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ookbee.Ads.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool HasValue(this object source)
        {
            return !(source == null || string.IsNullOrWhiteSpace(source.ToString()));
        }

        public static bool HasValue<TSource>(this ICollection<TSource> source)
        {
            return !(source == null || !source.Any());
        }

        public static bool HasValue<TSource>(this IEnumerable<TSource> source)
        {
            return !(source == null || !source.Any());
        }

        public static byte[] ToBytes(this object obj)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }
    }
}
