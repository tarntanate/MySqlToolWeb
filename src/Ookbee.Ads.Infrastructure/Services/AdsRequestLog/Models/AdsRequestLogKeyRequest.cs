using Newtonsoft.Json;

namespace Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models
{
    public class AdGroupRequestLogKeyRequest
    {
        [JsonProperty("uuid")]
        public string UUID { get; set; }
    }
}