using Newtonsoft.Json;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerDto
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BannerAdDto Ad { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BannerAnalyticsDto Analytics { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string AdUnitType { get; set; }
    }
}
