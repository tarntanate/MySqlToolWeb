using System;
using System.Linq.Expressions;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost
{
    public class CampaignCostDto
    {
        public long Id { get; set; }
        public long CampaignId { get; set; }
        public decimal Budget { get; set; }
        public decimal CostPerUnit { get; set; }

        public static Expression<Func<CampaignCostEntity, CampaignCostDto>> Projection
        {
            get
            {
                return entity => new CampaignCostDto()
                {
                    Id = entity.Id,
                    CampaignId = entity.CampaignId,
                    Budget = entity.Budget,
                    CostPerUnit = entity.CostPerUnit
                };
            }
        }
    }
}
