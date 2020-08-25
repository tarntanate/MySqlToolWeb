using MediatR;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.Cache.Commands.AdUnitStatsCache
{
    public class UpdateAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public AdStats Stats { get; set; }

        public UpdateAdUnitStatsCacheCommand(long adUnitId, AdStats stats)
        {
            AdUnitId = adUnitId;
            Stats = stats;
        }
    }
}
