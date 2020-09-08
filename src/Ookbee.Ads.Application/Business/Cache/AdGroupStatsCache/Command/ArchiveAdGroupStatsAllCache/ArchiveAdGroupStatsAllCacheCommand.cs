using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsAllCache
{
    public class ArchiveAdGroupStatsAllCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }

        public ArchiveAdGroupStatsAllCacheCommand(DateTime caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
