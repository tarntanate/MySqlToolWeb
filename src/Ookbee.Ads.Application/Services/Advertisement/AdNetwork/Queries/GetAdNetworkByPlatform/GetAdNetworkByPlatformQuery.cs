using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform
{
    public class GetAdNetworkByPlatformQuery : IRequest<Response<AdNetworkDto>>
    {
        public long AdUnitId { get; private set; }
        public AdPlatform Platform { get; private set; }

        public GetAdNetworkByPlatformQuery(long adUnitId, AdPlatform platform)
        {
            AdUnitId = adUnitId;
            Platform = platform;
        }
    }
}
