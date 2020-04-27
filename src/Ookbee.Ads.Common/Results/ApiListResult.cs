using Newtonsoft.Json;

namespace Ookbee.Ads.Common.Result
{
    public class ApiListResult<T> : ApiErrorResult
    {
        [JsonProperty(Order = -1)]
        public ApiItemsResult<T> Data { get; set; }
    }
}
