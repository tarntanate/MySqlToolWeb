using Anna.Common.MongoDB.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.MongoDB
{
    [CollectionName("CampaignItemAsset")]
    public class CampaignItemAssetDocument : BaseDocument
    {
        public string CampaignItemId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string MediaType { get; set; }

        public string MediaUrl { get; set; }

        public string LinkUrl { get; set; }

        public string Position { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

        public bool EnableFlag { get; set; }
    }
}