using Anna.Common.MongoDB.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ookbee.Ads.Common;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("Ad")]
    [BsonIgnoreExtraElements]
    public class AdDocument : BaseDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string CampaignId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AdSlotId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan? Cooldown { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public List<string> Analytics { get; set; }

        public string AppLink { get; set; }

        public string WebLink { get; set; }

        public PlatformModel Platform { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnabledFlag { get; set; }
    }
}