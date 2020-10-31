using Newtonsoft.Json;

namespace Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models
{
    public class AdGroupRequestLogRecordRequest
    {
        [JsonProperty("key")]
        public AdGroupRequestLogKeyRequest Key { get; set; }
        [JsonProperty("value")]
        public AdGroupRequestLogValueRequest Value { get; set; }

    }
}