using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Queries.IsExistsCampaignCostById
{
    public class IsExistsCampaignCostByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsCampaignCostByIdQuery(long id)
        {
            Id = id;
        }
    }
}
