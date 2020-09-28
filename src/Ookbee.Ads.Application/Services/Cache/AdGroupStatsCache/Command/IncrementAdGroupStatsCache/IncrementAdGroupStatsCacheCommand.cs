using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache
{
    public class IncrementAdGroupStatsCacheCommand : IRequest<Response<bool>>
    {
        public AdStatsType StatsType { get; set; }
        public long AdGroupId { get; set; }

        public IncrementAdGroupStatsCacheCommand(AdStatsType statsType, long adGroupId)
        {
            StatsType = statsType;
            AdGroupId = adGroupId;
        }
    }
}
