using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommand : IRequest<Response<bool>>
    {
        public AdStatsType StatsType { get; set; }
        public long AdUnitId { get; set; }

        public IncrementAdUnitStatsCacheCommand(AdStatsType statsType, long adUnitId)
        {
            StatsType = statsType;
            AdUnitId = adUnitId;
        }
    }
}
