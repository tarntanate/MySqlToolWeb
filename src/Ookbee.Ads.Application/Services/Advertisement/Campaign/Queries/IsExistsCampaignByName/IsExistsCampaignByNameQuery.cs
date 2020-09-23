using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.IsExistsCampaignByName
{
    public class IsExistsCampaignByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; set; }

        public IsExistsCampaignByNameQuery(string name)
        {
            Name = name;
        }
    }
}
