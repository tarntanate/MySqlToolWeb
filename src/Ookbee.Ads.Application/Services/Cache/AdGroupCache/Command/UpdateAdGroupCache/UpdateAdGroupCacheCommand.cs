using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupCache.Commands.UpdateAdGroupCache
{
    public class UpdateAdGroupCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public UpdateAdGroupCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
