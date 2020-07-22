using Newtonsoft.Json;

namespace Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models
{
    public class OffsetsResponse
    {
        [JsonProperty("partition")]
        public int Partition { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
    }
}
