using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCacheByPlatform
{
    public class IncrementAdStatsCacheByPlatformCommand : IRequest<Unit>
    {
        public StatsType StatsType { get; set; }
        public long AdId { get; set; }

        public IncrementAdStatsCacheByPlatformCommand(StatsType statsType, long adId)
        {
            StatsType = statsType;
            AdId = adId;
        }
    }
}
