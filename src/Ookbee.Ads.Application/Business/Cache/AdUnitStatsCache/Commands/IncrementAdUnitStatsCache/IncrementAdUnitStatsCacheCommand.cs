using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }
        public AdStatsType Stats { get; set; }

        public IncrementAdUnitStatsCacheCommand(long adUnitId, Platform platform, AdStatsType stats)
        {
            AdUnitId = adUnitId;
            Platform = platform;
            Stats = stats;
        }
    }
}
