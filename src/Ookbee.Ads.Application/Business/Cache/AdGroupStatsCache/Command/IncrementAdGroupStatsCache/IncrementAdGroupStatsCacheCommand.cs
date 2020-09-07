using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache
{
    public class IncrementAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public AdStatsType Stats { get; set; }

        public IncrementAdGroupStatsCacheCommand(long adGroupId, Platform platform, AdStatsType stats)
        {
            AdGroupId = adGroupId;
            Platform = platform;
            Stats = stats;
        }
    }
}
