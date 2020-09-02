using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public AdStats Stats { get; set; }

        public IncrementAdUnitStatsCacheCommand(long adUnitId, AdStats stats)
        {
            AdUnitId = adUnitId;
            Stats = stats;
        }
    }
}
