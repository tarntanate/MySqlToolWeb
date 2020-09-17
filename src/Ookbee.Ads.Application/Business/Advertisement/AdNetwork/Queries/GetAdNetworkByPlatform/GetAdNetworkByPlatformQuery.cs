using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform
{
    public class GetAdNetworkByPlatformQuery : IRequest<HttpResult<AdNetworkDto>>
    {
        public Platform Platform { get; set; }

        public GetAdNetworkByPlatformQuery(Platform platform)
        {
            Platform = platform;
        }
    }
}
