using System;
using Anna.Common.MongoDB.Domain;
using MongoDB.Bson.Serialization.Attributes;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("UploadUrl")]
    public class UploadUrlDocument : BaseDocument
    {
        public string MapperId { get; set; }

        public string AppId { get; set; }
        
        public string Region { get; set; }

        public string Bucket { get; set; }

        public string Key { get; set; }

        public string SignedUrl { get; set; }

        public string SignedUrlCommit { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }
    }
}