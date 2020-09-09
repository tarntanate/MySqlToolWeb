using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.ArchiveAdStatsByUnitIdCache
{
    public class ArchiveAdStatsByUnitIdCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdUnitId { get; set; }

        public ArchiveAdStatsByUnitIdCacheCommand(DateTime caculatedAt, long adUnitId)
        {
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
        }
    }
}
