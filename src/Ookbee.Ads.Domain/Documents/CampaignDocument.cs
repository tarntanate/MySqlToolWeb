using Anna.Common.MongoDB.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("Campaign")]
    [BsonIgnoreExtraElements]
    public class CampaignDocument : BaseDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string AdvertiserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
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
    }
}
