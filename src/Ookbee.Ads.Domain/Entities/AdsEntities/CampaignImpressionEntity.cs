using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class CampaignImpressionEntity : BaseEntity
    {
        public long CampaignId { get; set; }
        public int Quota { get; set; }

        public virtual CampaignEntity Campaign { get; set; }
    }
}