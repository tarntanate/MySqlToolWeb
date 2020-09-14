using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public Platform Platform { get; set; }
        public StatsType StatsType { get; set; }
        public long AdUnitId { get; set; }

        public IncrementAdUnitStatsCacheCommand(Platform platform, StatsType statsType, long adUnitId)
        {
            Platform = platform;
            StatsType = statsType;
            AdUnitId = adUnitId;
        }
    }
}
