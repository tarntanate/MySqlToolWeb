using Newtonsoft.Json;
using Ookbee.Ads.Infrastructure.Enums;
using System.Collections.Generic;

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
