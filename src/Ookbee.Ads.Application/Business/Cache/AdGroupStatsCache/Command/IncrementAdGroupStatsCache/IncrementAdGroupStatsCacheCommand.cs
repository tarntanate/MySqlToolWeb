using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache
{
    public class IncrementAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public Platform Platform { get; set; }
        public StatsType StatsType { get; set; }
        public long AdGroupId { get; set; }

        public IncrementAdGroupStatsCacheCommand(Platform platform, StatsType statsType, long adGroupId)
        {
            Platform = platform;
            StatsType = statsType;
            AdGroupId = adGroupId;
        }
    }
}
