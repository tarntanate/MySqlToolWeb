namespace Ookbee.Ads.Application.Business.CampaignCost
{
    public class CampaignCostDto
    {
        public long Id { get; set; }
        public long CampaignId { get; set; }
        public decimal Budget { get; set; }
        public decimal CostPerUnit { get; set; }
    }
}
