using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsByIdCache
{
    public class ArchiveAdGroupStatsByIdCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdGroupId { get; set; }

        public ArchiveAdGroupStatsByIdCacheCommand(DateTime caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
