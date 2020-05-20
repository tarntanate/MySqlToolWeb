using Anna.Common.MongoDB.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("AdSlot")]
    public class AdSlotDocument : BaseDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string PublisherId { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string SlotTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnabledFlag { get; set; }
    }
}