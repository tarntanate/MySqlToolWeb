using System;
using System.Text.Json.Serialization;

namespace Ookbee.Ads.Application.Business.Advertiser
{
    public class AdvertiserDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Contact { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public bool EnabledFlag { get; set; }
    }
}
