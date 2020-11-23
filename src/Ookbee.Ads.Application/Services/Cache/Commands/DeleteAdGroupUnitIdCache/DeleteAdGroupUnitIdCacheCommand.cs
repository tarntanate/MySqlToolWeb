using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupUnitIdCache
{
    public class DeleteAdGroupUnitIdCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }
        public long AdUnitId { get; private set; }

        public DeleteAdGroupUnitIdCacheCommand(long adGroupId, long adUnitId)
        {
            AdGroupId = adGroupId;
            AdUnitId = adUnitId;
        }
    }
}
