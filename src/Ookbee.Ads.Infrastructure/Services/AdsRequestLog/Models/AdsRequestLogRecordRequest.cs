using Newtonsoft.Json;

namespace Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models
{
    public class AdsRequestLogRecordRequest
    {
        [JsonProperty("key")]
        public AdsRequestLogKeyRequest Key { get; set; }
        [JsonProperty("value")]
        public AdsRequestLogValueRequest Value { get; set; }

    }
}