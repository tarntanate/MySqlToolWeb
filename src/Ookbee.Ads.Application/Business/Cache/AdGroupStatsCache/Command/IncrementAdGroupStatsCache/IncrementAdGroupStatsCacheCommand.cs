using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache
{
    public class IncrementAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public StatsType StatsType { get; set; }

        public IncrementAdGroupStatsCacheCommand(long adGroupId, Platform platform, StatsType statsType)
        {
            AdGroupId = adGroupId;
            Platform = platform;
            StatsType = statsType;
        }
    }
}
