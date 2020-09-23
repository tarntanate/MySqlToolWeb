using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.InitialAdCache
{
    public class InitialAdCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public InitialAdCacheCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
