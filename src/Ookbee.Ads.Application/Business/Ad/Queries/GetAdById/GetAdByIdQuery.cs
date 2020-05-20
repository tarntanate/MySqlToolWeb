using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdById
{
    public class GetAdByIdQuery : IRequest<HttpResult<AdDto>>
    {
        public string Id { get; set; }
        
        public string CampaignId { get; set; }

        public GetAdByIdQuery(string campaignId, string id)
        {
            Id = id;
            CampaignId = campaignId;
        }
    }
}
