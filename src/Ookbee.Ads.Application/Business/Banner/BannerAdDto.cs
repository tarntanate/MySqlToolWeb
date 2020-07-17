
using Newtonsoft.Json;
using Ookbee.Ads.Application.Business.AdAsset;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Enums;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerAdDto : DefaultDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public AdStatus Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CountdownSecond { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ForegroundColor { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BackgroundColor { get; set; }

        public string LinkUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BannerAnalyticsDto Analytics { get; set; }

        public IEnumerable<Platform> Platforms { get; set; }

        public IEnumerable<BannerAssetDto> Assets { get; set; }
    }
}
