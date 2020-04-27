using Newtonsoft.Json;

namespace Ookbee.Ads.Common.Result
{
    public abstract class ApiBaseResult
    {
        [JsonProperty(Order = -4)]
        public string ApiVersion { get; set; } = "1.0";

        [JsonProperty(Order = -3)]
        public abstract bool Ok { get; }
    }
}
