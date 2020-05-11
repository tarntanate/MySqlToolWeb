using Anna.Common.MongoDB.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.MongoDB
{
    [CollectionName("PricingModel")]
    public class PricingModelDocument : BaseDocument
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnableFlag { get; set; }
    }
}