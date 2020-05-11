using Anna.Common.MongoDB.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.MongoDB
{
    [CollectionName("Banner")]
    public class BannerDocument : BaseDocument
    {
        public string CampaignId { get; set; }

        public string UnitTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Cooldown { get; set; }

        public string Position { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnableFlag { get; set; }
    }
}