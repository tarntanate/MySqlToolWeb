using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.UpdateAdUnitCache
{
    public class UpdateAdUnitCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public UpdateAdUnitCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
