using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCache
{
    public class IncrementAdStatsCacheCommand : IRequest<Unit>
    {
        public StatsType StatsType { get; set; }
        public long AdId { get; set; }

        public IncrementAdStatsCacheCommand(StatsType statsType, long adId)
        {
            StatsType = statsType;
            AdId = adId;
        }
    }
}
