using Newtonsoft.Json;
using Ookbee.Ads.Application.Business.MediaFile;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Ad
{
    public class AdItemDto
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

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }

        public IEnumerable<MediaFileDto> MediaFiles { get; set; }
    }
}