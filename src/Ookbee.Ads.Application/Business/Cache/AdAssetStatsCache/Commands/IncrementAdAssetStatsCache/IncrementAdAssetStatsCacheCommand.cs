using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.IncrementAdAssetStatsCache
{
    public class IncrementAdAssetStatsCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public Platform Platform { get; set; }
        public AdStatsType Stats { get; set; }

        public IncrementAdAssetStatsCacheCommand(long adId, Platform platform, AdStatsType stats)
        {
            AdId = adId;
            Platform = platform;
            Stats = stats;
        }
    }
}
