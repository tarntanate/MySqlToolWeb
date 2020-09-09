using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }
        public StatsType StatsType { get; set; }

        public IncrementAdUnitStatsCacheCommand(long adUnitId, Platform platform, StatsType statsType)
        {
            AdUnitId = adUnitId;
            Platform = platform;
            StatsType = statsType;
        }
    }
}
