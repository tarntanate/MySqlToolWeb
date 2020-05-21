using System;
using Anna.Common.MongoDB.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("UploadUrl")]
    public class UploadUrlDocument : BaseDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string MapperId { get; set; }

        public string MapperType { get; set; }

        public string AppId { get; set; }

        public string Region { get; set; }

        public string Bucket { get; set; }

        public string Key { get; set; }

        public string SignedUrl { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }
    }
}