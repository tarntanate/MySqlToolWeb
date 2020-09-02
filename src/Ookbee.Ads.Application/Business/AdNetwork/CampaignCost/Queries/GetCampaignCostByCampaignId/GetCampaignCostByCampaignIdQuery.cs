using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Queries.GetCampaignCostById
{
    public class GetCampaignCostByCampaignIdQuery : IRequest<HttpResult<CampaignCostDto>>
    {
        public long CampaignId { get; set; }

        public GetCampaignCostByCampaignIdQuery(long id)
        {
            CampaignId = id;
        }
    }
}
