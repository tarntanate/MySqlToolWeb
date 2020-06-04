using Newtonsoft.Json;
using System;

namespace Ookbee.Ads.Application.Business.SlotType
{
    public class SlotTypeDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }
}
