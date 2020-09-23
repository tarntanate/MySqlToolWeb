using Newtonsoft.Json;

namespace Ookbee.Ads.Common.Response
{
    public class ApiItemResult<T> : ApiErrorResult
    {
        [JsonProperty(Order = -1)]
        public T Data { get; set; }
    }
}
