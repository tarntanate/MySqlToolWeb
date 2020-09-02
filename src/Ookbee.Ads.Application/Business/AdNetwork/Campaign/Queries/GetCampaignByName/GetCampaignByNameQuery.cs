using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignByName
{
    public class GetCampaignByNameQuery : IRequest<HttpResult<CampaignDto>>
    {
        public string Name { get; set; }

        public GetCampaignByNameQuery(string name)
        {
            Name = name;
        }
    }
}
