using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCache
{
    public class IncrementAdStatsCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public Platform Platform { get; set; }
        public AdStatsType Stats { get; set; }

        public IncrementAdStatsCacheCommand(long adId, Platform platform, AdStatsType stats)
        {
            AdId = adId;
            Platform = platform;
            Stats = stats;
        }
    }
}
