using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerAdDto
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CountdownSecond { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ForegroundColor { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BackgroundColor { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LinkUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<BannerAssetDto> Assets { get; set; }
    }
}
