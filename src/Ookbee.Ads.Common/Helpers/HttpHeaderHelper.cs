using System.Collections.Generic;
using System.Linq;

namespace Ookbee.Ads.Common.Helpers
{
    public class HttpHeaderHelper
    {
        public static string GetHeader(string key)
        {
            string result = null;
            var value = HttpContextHelper.Current?.Request?.Headers[key];
            if (!string.IsNullOrEmpty(value))
            {
                result = value.ToString().Trim();
            }
            return result;
        }

        public static Dictionary<string, string> GetHeaders()
        {
            var result = new Dictionary<string, string>();
            var headers = HttpContextHelper.Current?.Request?.Headers;
            return headers.Select(x => new KeyValuePair<string, string>(x.Key, x.Value)).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
