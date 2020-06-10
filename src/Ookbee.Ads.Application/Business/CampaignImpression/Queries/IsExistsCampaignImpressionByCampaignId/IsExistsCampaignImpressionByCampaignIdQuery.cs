using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Queries.IsExistsCampaignImpressionByCampaignId
{
    public class IsExistsCampaignImpressionByCampaignIdQuery : IRequest<HttpResult<bool>>
    {
        public long CampaignId { get; set; }

        public IsExistsCampaignImpressionByCampaignIdQuery(long campaignId)
        {
            CampaignId = campaignId;
        }
    }
}
