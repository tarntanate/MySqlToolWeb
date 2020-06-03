using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Ookbee.Ads.Application.Business.Campaign
{
    public class CampaignDto
    {
        public string Id { get; set; }

        public string AdvertiserId { get; set; }

        public string PricingModelId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Budget { get; set; }

        public int LimitViewTotal { get; set; }

        public int LimitViewPerPerson { get; set; }

        public TimeSpan LimitViewResetAfter { get; set; }

        public int PricingClick { get; set; }

        public int PricingImpressions { get; set; }

        public decimal PricingRate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }
}
