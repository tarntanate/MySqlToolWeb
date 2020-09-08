using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsByGroupIdCache
{
    public class ArchiveAdUnitStatsByGroupIdCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdGroupId { get; set; }

        public ArchiveAdUnitStatsByGroupIdCacheCommand(DateTime caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
