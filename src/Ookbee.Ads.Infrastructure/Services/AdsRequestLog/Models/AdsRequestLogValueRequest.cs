using System;
using Newtonsoft.Json;

namespace Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models
{
    public class AdsRequestLogValueRequest
    {
        [JsonProperty("CreatedAt")]
        public string CreatedAt { get; set; }
        public int AdId { get; set; }
        public int AdUnitId { get; set; }
        public int UnitId { get; set; }
        public int AdsGroupId { get; set; }
        public int PublisherId { get; set; }
        public int PlatformId { get; set; }
        public int CampaignId { get; set; }
        public int RequestTypeId { get; set; }

        [JsonProperty("uuid")]
        public string UUID { get; set; }
    }

}