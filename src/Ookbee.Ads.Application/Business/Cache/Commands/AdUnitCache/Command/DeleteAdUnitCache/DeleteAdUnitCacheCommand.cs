using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.DeleteAdUnitCache
{
    public class DeleteAdUnitCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public string Unit { get; set; }

        public DeleteAdUnitCacheCommand(long adGroupId, string unit)
        {
            AdGroupId = adGroupId;
            Unit = unit;
        }
    }
}
