using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ookbee.Ads.Common.Helpers
{
    public class JsonHelper
    {
        public static TResult Deserialize<TResult>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }
            var result = JsonConvert.DeserializeObject<TResult>(json);
            return result;
        }

        public static string Serialize(object obj)
        {
            if (obj == null)
            {
                return default;
            }
            var serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var result = JsonConvert.SerializeObject(obj, serializerSettings);
            return result;
        }

    }
}
