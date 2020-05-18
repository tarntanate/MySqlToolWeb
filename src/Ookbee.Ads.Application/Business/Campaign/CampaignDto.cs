using MongoDB.Bson.Serialization.Attributes;
using Ookbee.Ads.Application.Infrastructure;
using System;

namespace Ookbee.Ads.Application.Business.Campaign
{
    public class CampaignDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Budget { get; set; }

        public int LimitViewTotal { get; set; }

        public int LimitViewPerPerson { get; set; }

        public TimeSpan LimitViewResetAfter { get; set; }

        public int PricingClick { get; set; }

        public int PricingImpressions { get; set; }

        public decimal PricingRate { get; set; }

        public bool IsExpire { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndDate { get; set; }

        public DefaultDto Advertiser{ get; set; }

        public DefaultDto PricingModel { get; set; }
    }
}
