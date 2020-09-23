using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache
{
    public class IncrementAdGroupStatsCacheCommand : IRequest<Response<bool>>
    {
        public StatsType StatsType { get; set; }
        public long AdGroupId { get; set; }

        public IncrementAdGroupStatsCacheCommand(StatsType statsType, long adGroupId)
        {
            StatsType = statsType;
            AdGroupId = adGroupId;
        }
    }
}
