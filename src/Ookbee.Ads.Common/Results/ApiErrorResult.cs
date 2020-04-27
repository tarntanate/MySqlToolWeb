using Newtonsoft.Json;

namespace Ookbee.Ads.Common.Result
{
    public class ApiErrorResult : ApiBaseResult
    {
        [JsonProperty(Order = -2)]
        public override bool Ok => false;

        [JsonProperty(Order = -1)]
        public ApiErrorInfoResult Error { get; set; }
    }
}
