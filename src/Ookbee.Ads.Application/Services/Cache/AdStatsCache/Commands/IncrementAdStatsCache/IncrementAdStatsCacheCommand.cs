using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.IncrementAdStatsCache
{
    public class IncrementAdStatsCacheCommand : IRequest<Response<bool>>
    {
        public AdStatsType StatsType { get; set; }
        public long AdId { get; set; }

        public IncrementAdStatsCacheCommand(AdStatsType statsType, long adId)
        {
            StatsType = statsType;
            AdId = adId;
        }
    }
}
