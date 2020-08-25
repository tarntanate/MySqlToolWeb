using MediatR;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.Cache.Commands.AdAssetsStatsCache
{
    public class UpdateAdAssetsStatsCacheCommand : IRequest<Unit>
    {
        public long UnitId { get; set; }
        public long AdId { get; set; }
        public AdStats Stats { get; set; }

        public UpdateAdAssetsStatsCacheCommand(long unitId, long adId, AdStats stats)
        {
            UnitId = unitId;
            AdId = adId;
            Stats = stats;
        }
    }
}
