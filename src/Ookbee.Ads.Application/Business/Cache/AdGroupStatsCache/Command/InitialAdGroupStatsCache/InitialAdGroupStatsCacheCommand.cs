using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.InitialAdGroupStatsCache
{
    public class InitialAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public AdStats Stats { get; set; }

        public InitialAdGroupStatsCacheCommand(long adGroupId, Platform platform, AdStats stats)
        {
            AdGroupId = adGroupId;
            Platform = platform;
            Stats = stats;
        }
    }
}
