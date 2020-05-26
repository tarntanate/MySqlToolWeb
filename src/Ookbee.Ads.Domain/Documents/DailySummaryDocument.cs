using System;
using Anna.Common.MongoDB.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("DailySummary")]
    public class DailySummaryDocument : BaseDocument
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public string CampaignId { get; set; }

        public int Requests { get; set; }

        public int Fills { get; set; }

        public int Impressions { get; set; }

        public int Clicks { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CaculateDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnabledFlag { get; set; }
    }
}