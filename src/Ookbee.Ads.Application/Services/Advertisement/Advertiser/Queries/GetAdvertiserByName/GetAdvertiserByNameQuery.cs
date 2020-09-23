using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.GetAdvertiserByName
{
    public class GetAdvertiserByNameQuery : IRequest<Response<AdvertiserDto>>
    {
        public string Name { get; private set; }

        public GetAdvertiserByNameQuery(string name)
        {
            Name = name;
        }
    }
}
