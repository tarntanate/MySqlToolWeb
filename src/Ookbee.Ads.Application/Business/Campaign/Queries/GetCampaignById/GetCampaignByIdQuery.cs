using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById
{
    public class GetCampaignByIdQuery : IRequest<HttpResult<CampaignDto>>
    {
        public string Id { get; set; }

        public GetCampaignByIdQuery(string id)
        {
            Id = id;
        }
    }
}
