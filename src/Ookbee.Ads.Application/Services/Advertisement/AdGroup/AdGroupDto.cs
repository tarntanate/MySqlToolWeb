using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType;
using Ookbee.Ads.Application.Services.Advertisement.Publisher;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup
{
    public class AdGroupDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Placement { get; set; }
        public bool Enabled { get; set; }
        public int TotalAdUnit { get; set; }
        public AdGroupTypeDto AdGroupType { get; set; }
        public PublisherDto Publisher { get; set; }
    }
}
