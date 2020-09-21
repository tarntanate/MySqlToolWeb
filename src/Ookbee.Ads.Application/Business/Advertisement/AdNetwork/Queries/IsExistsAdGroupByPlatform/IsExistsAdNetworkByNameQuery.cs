using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.IsExistsAdNetworkByPlatform
{
    public class IsExistsAdNetworkByPlatformQuery : IRequest<HttpResult<bool>>
    {
        public Platform Platform { get; set; }

        public IsExistsAdNetworkByPlatformQuery(Platform platform)
        {
            Platform = platform;
        }
    }
}
