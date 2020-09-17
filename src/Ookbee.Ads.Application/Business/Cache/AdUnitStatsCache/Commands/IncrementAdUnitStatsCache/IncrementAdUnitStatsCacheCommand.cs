using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public StatsType StatsType { get; set; }
        public long AdUnitId { get; set; }

        public IncrementAdUnitStatsCacheCommand(StatsType statsType, long adUnitId)
        {
            StatsType = statsType;
            AdUnitId = adUnitId;
        }
    }
}
