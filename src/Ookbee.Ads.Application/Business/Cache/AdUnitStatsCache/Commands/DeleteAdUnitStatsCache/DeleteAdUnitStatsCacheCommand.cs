using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.DeleteAdUnitStatsCache
{
    public class DeleteAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public DeleteAdUnitStatsCacheCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
