using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression.Queries.GetCampaignImpressionById
{
    public class GetCampaignImpressionByCampaignIdQuery : IRequest<HttpResult<CampaignImpressionDto>>
    {
        public long CampaignId { get; set; }

        public GetCampaignImpressionByCampaignIdQuery(long id)
        {
            CampaignId = id;
        }
    }
}
