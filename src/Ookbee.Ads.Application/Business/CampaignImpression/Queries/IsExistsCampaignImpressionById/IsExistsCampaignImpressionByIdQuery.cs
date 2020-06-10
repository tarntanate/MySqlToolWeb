using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Queries.IsExistsCampaignImpressionById
{
    public class IsExistsCampaignImpressionByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsCampaignImpressionByIdQuery(long id)
        {
            Id = id;
        }
    }
}
