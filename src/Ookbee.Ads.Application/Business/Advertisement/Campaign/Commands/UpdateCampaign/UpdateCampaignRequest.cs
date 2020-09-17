using System;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignRequest
    {
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
