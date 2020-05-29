using System;
using System.Text.Json.Serialization;

namespace Ookbee.Ads.Application.Business.SlotType
{
    public class SlotTypeDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public bool EnabledFlag { get; set; }
    }
}
