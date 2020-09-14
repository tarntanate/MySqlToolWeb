using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCacheByPlatform
{
    public class IncrementAdStatsCacheByPlatformCommand : IRequest<Unit>
    {
        public Platform Platform { get; set; }
        public StatsType StatsType { get; set; }
        public long AdId { get; set; }

        public IncrementAdStatsCacheByPlatformCommand(Platform platform, StatsType statsType, long adId)
        {
            Platform = platform;
            StatsType = statsType;
            AdId = adId;
        }
    }
}
