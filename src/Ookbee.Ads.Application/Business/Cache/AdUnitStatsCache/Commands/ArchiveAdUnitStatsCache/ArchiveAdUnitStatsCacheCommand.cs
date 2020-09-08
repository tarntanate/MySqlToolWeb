using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsCache
{
    public class ArchiveAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public ArchiveAdUnitStatsCacheCommand(long adUnitId, DateTime caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
