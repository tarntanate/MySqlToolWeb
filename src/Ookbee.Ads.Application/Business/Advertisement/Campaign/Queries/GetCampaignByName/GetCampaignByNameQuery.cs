using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Queries.GetCampaignByName
{
    public class GetCampaignByNameQuery : IRequest<Response<CampaignDto>>
    {
        public string Name { get; set; }

        public GetCampaignByNameQuery(string name)
        {
            Name = name;
        }
    }
}
