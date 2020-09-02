using System;
using System.Linq.Expressions;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression
{
    public class CampaignImpressionDto
    {
        public long Id { get; set; }
        public long CampaignId { get; set; }
        public decimal Quota { get; set; }

        public static Expression<Func<CampaignImpressionEntity, CampaignImpressionDto>> Projection
        {
            get
            {
                return entity => new CampaignImpressionDto()
                {
                    Id = entity.Id,
                    CampaignId = entity.CampaignId,
                    Quota = entity.Quota,
                };
            }
        }
    }
}
