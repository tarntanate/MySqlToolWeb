using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsCache
{
    public class ArchiveAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }

        public ArchiveAdGroupStatsCacheCommand(DateTime caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
