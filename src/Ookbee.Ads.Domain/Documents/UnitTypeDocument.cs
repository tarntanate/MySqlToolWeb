using Anna.Common.MongoDB.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ookbee.Ads.Domain.Documents
{
    [CollectionName("SlotType")]
    public class SlotTypeDocument : BaseDocument
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
