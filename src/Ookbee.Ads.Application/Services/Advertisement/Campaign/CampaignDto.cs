using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Advertiser;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign
{
    public class CampaignDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalAds { get; set; }
        public AdvertiserDto Advertiser { get; set; }
    }
}