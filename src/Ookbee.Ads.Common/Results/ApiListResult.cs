using Newtonsoft.Json;

namespace Ookbee.Ads.Common.Response
{
    public class ApiListResult<T> : ApiErrorResult
    {
        [JsonProperty(Order = -1)]
        public ApiItemsResult<T> Data { get; set; } = new ApiItemsResult<T>();
    }
}
