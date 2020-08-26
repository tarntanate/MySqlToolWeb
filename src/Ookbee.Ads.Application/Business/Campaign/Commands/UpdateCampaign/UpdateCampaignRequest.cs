using System;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignRequest
    {
        public long AdvertiserId { get; set; }
        public PricingModel PricingModel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
