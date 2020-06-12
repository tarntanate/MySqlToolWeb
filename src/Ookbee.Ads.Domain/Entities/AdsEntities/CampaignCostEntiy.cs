using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class CampaignCostEntity : BaseEntity
    {
        public long CampaignId { get; set; }
        public decimal Budget { get; set; }
        public decimal CostPerUnit { get; set; }

        public virtual CampaignEntity Campaign { get; set; }
    }
}
