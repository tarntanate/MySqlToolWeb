using System;
using Newtonsoft.Json;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class DefaultDto
    {
        [JsonProperty(Order = -99)]
        public long Id { get; set; }
        [JsonIgnore]
        public DateTimeOffset? CreatedAt { get; set; }
        [JsonIgnore]
        public DateTimeOffset? UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTimeOffset? DeletedAt { get; set; }
    }
}