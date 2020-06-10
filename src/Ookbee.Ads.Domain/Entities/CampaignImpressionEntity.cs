using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities
{
    public class CampaignImpressionEntity : BaseEntity
    {
        public long CampaignId { get; set; }
        public int Quota { get; set; }

        public virtual CampaignEntity Campaign { get; set; }
    }
}
