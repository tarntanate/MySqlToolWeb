using Anna.Common.MongoDB.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("MediaFile")]
    public class MediaFileDocument : BaseDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string AdId { get; set; }

        public string MimeType { get; set; }

        public string MediaUrl { get; set; }

        public string Position { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnabledFlag { get; set; }
    }
}