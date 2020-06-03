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

        public string MimeType { get; set; }

        public string FileExtension { get; set; }

        public string AppId { get; set; }

        public string Region { get; set; }

        public string SourceBucket { get; set; }

        public string SourceKey { get; set; }

        public string DestinationBucket { get; set; }

        public string DestinationKey { get; set; }

        public string SignedUrl { get; set; }
    }
}
