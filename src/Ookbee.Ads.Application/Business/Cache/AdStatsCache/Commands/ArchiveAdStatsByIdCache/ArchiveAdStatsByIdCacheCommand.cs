using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.ArchiveAdStatsByIdCache
{
    public class ArchiveAdStatsByIdCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdId { get; set; }

        public ArchiveAdStatsByIdCacheCommand(DateTime caculatedAt, long adId)
        {
            CaculatedAt = caculatedAt;
            AdId = adId;
        }
    }
}
