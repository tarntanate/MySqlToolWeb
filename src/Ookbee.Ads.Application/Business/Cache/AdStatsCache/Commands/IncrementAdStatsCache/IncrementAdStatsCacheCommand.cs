using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCache
{
    public class IncrementAdStatsCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public Platform Platform { get; set; }
        public StatsType StatsType { get; set; }

        public IncrementAdStatsCacheCommand(long adId, Platform platform, StatsType statsType)
        {
            AdId = adId;
            Platform = platform;
            StatsType = statsType;
        }
    }
}
