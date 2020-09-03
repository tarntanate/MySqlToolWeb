using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetsStatsCache.Commands.IncrementAdAssetsStatsCache
{
    public class IncrementAdAssetsStatsCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public AdStats Stats { get; set; }

        public IncrementAdAssetsStatsCacheCommand(long adId, AdStats stats)
        {
            AdId = adId;
            Stats = stats;
        }
    }
}
