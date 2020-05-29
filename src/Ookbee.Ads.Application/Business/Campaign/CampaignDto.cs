using MongoDB.Bson.Serialization.Attributes;
using Ookbee.Ads.Application.Infrastructure;
using System;
using System.Text.Json.Serialization;

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
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public bool EnabledFlag { get; set; }
    }
}
