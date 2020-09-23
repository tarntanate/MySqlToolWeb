using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform
{
    public class GetAdNetworkByPlatformQuery : IRequest<Response<AdNetworkDto>>
    {
        public Platform Platform { get; set; }

        public GetAdNetworkByPlatformQuery(Platform platform)
        {
            Platform = platform;
        }
    }
}
