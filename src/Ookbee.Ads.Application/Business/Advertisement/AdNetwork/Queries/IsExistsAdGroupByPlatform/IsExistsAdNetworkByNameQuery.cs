using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.IsExistsAdNetworkByPlatform
{
    public class IsExistsAdNetworkByPlatformQuery : IRequest<Response<bool>>
    {
        public Platform Platform { get; set; }

        public IsExistsAdNetworkByPlatformQuery(Platform platform)
        {
            Platform = platform;
        }
    }
}
