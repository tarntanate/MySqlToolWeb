using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ookbee.Ads.Common.Helpers
{
    public class JsonHelper
    {
        public static TResult Deserialize<TResult>(string json)
        {
            if (string.IsNullOrEmpty(json))
                return default;

            var result = JsonConvert.DeserializeObject<TResult>(json);
            return result;
        }

        public static string Serialize(object obj)
        {
            if (obj == null)
                return default;

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new StringEnumConverter());
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var result = JsonConvert.SerializeObject(obj, serializerSettings);
            return result;
        }

    }
}