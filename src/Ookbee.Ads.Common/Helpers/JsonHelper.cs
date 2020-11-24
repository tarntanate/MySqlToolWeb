using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Ookbee.Ads.Common.Helpers
{
    public static class JsonHelper
    {
        public static TResult Deserialize<TResult>(string json)
        {
            if (string.IsNullOrEmpty(json))
                return default;

            var result = JsonConvert.DeserializeObject<TResult>(json);
            return result;
        }

        public static string Serialize(object obj, bool useCamelCasePropertyName = true)
        {
            if (obj == null)
                return default;

            var serializerSettings = new JsonSerializerSettings();
            if (useCamelCasePropertyName) {
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            serializerSettings.Converters.Add(new StringEnumConverter());
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var result = JsonConvert.SerializeObject(obj, serializerSettings);
            return result;
        }

    }
}