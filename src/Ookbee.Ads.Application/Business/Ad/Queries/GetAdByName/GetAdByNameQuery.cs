using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByName
{
    public class GetAdByNameQuery : IRequest<HttpResult<AdDto>>
    {
        public string CampaignId { get; set; }

        public string Name { get; set; }

        public GetAdByNameQuery(string campaignId, string name)
        {
            CampaignId = campaignId;
            Name = name;
        }
    }
}
