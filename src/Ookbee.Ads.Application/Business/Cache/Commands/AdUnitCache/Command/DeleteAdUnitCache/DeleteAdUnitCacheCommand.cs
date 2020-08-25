using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.DeleteAdUnitCache
{
    public class DeleteAdUnitCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public long AdUnitId { get; set; }

        public DeleteAdUnitCacheCommand(long adGroupId, long adUnitId)
        {
            AdGroupId = adGroupId;
            AdUnitId = adUnitId;
        }
    }
}
