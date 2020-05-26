using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ookbee.Ads.Common;

namespace Ookbee.Ads.Application.Business.Ad
{
    public class AdDto
    {
        public string Id { get; set; }

        public string CampaignId { get; set; }

        public string AdSlotId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TimeSpan? Cooldown { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ForegroundColor { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BackgroundColor { get; set; }

        public string AppLink { get; set; }

        public string WebLink { get; set; }

        public List<string> Analytics { get; set; }

        public PlatformModel Platform { get; set; }
    }
}