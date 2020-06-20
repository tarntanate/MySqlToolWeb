using System;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.CreateCampaignImpression
{
    public class CreateCampaignImpressionRequest
    {
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PricingModel PricingModel { get; set; }
        public int Qouta { get; set; }
    }
}
