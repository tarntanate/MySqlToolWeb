using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.InitialAdUnitCache
{
    public class InitialAdUnitCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public InitialAdUnitCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
