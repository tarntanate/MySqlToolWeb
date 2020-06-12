using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerAnalyticsDto
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Clicks { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Impressions { get; set; }
    }
}
