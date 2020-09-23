using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.GetCampaignByName
{
    public class GetCampaignByNameQuery : IRequest<Response<CampaignDto>>
    {
        public string Name { get; private set; }

        public GetCampaignByNameQuery(string name)
        {
            Name = name;
        }
    }
}
