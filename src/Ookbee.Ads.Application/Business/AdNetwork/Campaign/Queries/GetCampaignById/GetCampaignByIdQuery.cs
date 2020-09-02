using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignById
{
    public class GetCampaignByIdQuery : IRequest<HttpResult<CampaignDto>>
    {
        public long Id { get; set; }

        public GetCampaignByIdQuery(long id)
        {
            Id = id;
        }
    }
}
