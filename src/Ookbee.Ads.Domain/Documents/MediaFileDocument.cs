using Anna.Common.MongoDB.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("MediaFile")]
    public class MediaFileDocument : BaseDocument
    {
        public string AdId { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string MimeType { get; set; }

        public string MediaUrl { get; set; }

        public string Position { get; set; }

        public string AppLink { get; set; }

        public string WebLink { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnabledFlag { get; set; }
    }
}