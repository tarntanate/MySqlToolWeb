using Anna.Common.MongoDB.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("RequestLog")]
    public class RequestLogDocument : BaseDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string AdId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string KeyMetricId { get; set; }

        public string AppCode { get; set; }

        public string AppVersion { get; set; }

        public string Platform { get; set; }

        public string OsVersion { get; set; }

        public string DeviceId { get; set; }

        public string UserAgents { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnabledFlag { get; set; }
    }
}