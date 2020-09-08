using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.ArchiveAdAssetStatsByUnitIdCache
{
    public class ArchiveAdAssetStatsByUnitIdCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdUnitId { get; set; }

        public ArchiveAdAssetStatsByUnitIdCacheCommand(DateTime caculatedAt, long adUnitId)
        {
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
        }
    }
}
