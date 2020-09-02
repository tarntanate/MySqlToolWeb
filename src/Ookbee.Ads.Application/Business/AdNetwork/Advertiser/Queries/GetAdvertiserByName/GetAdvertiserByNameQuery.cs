using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Advertiser.Queries.GetAdvertiserByName
{
    public class GetAdvertiserByNameQuery : IRequest<HttpResult<AdvertiserDto>>
    {
        public string Name { get; set; }

        public GetAdvertiserByNameQuery(string name)
        {
            Name = name;
        }
    }
}
