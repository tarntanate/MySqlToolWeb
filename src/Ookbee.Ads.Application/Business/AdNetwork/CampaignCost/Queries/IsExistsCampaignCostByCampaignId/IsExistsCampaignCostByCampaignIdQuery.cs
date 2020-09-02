using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Queries.IsExistsCampaignCostByCampaignId
{
    public class IsExistsCampaignCostByCampaignIdQuery : IRequest<HttpResult<bool>>
    {
        public long CampaignId { get; set; }

        public IsExistsCampaignCostByCampaignIdQuery(long campaignId)
        {
            CampaignId = campaignId;
        }
    }
}
