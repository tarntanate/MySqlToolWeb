using System;
using Newtonsoft.Json;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class DefaultDto
    {
        [JsonProperty(Order = -99)]
        public long Id { get; set; }
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }
}