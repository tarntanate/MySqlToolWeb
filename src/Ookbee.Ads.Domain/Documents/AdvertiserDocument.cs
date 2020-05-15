using Anna.Common.MongoDB.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("Advertiser")]
    public class AdvertiserDocument : BaseDocument
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Contact { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnableFlag { get; set; }
    }
}