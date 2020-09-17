using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.InitialAdGroupCache
{
    public class InitialAdGroupCacheCommand : IRequest<Unit>
    {
        public InitialAdGroupCacheCommand(long adGroupId)
        {

        }
    }
}
