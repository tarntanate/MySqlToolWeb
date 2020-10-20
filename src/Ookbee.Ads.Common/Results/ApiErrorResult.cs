using Newtonsoft.Json;

namespace Ookbee.Ads.Common.Response
{
    public class ApiErrorResult : ApiBaseResult
    {
        [JsonProperty(Order = -1)]
        public ApiErrorInfoResult Error { get; set; }
    }
}
