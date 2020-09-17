using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.InitialAdCache
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
