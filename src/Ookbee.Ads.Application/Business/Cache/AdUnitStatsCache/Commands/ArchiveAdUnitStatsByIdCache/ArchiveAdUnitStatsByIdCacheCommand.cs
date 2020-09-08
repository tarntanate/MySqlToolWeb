using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsByIdCache
{
    public class ArchiveAdUnitStatsByIdCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdUnitId { get; set; }

        public ArchiveAdUnitStatsByIdCacheCommand(DateTime caculatedAt, long adUnitId)
        {
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
        }
    }
}
