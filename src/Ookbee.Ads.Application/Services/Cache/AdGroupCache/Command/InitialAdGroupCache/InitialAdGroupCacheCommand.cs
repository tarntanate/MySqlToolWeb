using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupCache.Commands.InitialAdGroupCache
{
    public class InitialAdGroupCacheCommand : IRequest<Unit>
    {
        public InitialAdGroupCacheCommand(long adGroupId)
        {

        }
    }
}
