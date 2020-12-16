using Newtonsoft.Json;

namespace Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models
{
    public class AdsRequestLogRecord
    {
        [JsonProperty("key")]
        public AdsRequestLogKey Key { get; set; }
        [JsonProperty("value")]
        public AdsRequestLogValue Value { get; set; }

    }
}