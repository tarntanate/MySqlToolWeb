using MediatR;

namespace Ookbee.Ads.Application.Services.CacheManager.AdGroupCache.Commands.CreateAdGroupIdCache
{
    public class CreateAdGroupIdCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }

        public CreateAdGroupIdCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
