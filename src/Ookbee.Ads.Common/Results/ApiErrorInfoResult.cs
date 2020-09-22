using Newtonsoft.Json;

namespace Ookbee.Ads.Common.Response
{
    public class ApiErrorInfoResult
    {
        [JsonProperty(Order = -5)]
        public int StatusCode { get; set; }

        [JsonProperty(Order = -4)]
        public string StatusMessage { get; set; }

        [JsonProperty(Order = -3)]
        public string Message { get; set; }

        [JsonProperty(Order = -2)]
        public object Reasons { get; set; }

        [JsonProperty(Order = -1)]
        public object Data { get; set; }
    }
}
