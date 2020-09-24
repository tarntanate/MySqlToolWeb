using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.IsExistsAdNetworkByPlatform
{
    public class IsExistsAdNetworkByPlatformQuery : IRequest<Response<bool>>
    {
        public AdPlatform Platform { get; private set; }

        public IsExistsAdNetworkByPlatformQuery(AdPlatform platform)
        {
            Platform = platform;
        }
    }
}
