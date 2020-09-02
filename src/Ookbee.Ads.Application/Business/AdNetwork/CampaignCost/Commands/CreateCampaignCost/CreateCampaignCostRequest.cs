using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Commands.CreateCampaignCost
{
    public class CreateCampaignCostRequest
    {
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PricingModel PricingModel { get; set; }
        public decimal Budget { get; set; }
        public decimal CostPerUnit { get; set; }
    }
}
