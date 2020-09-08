using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.ArchiveAdStatsCache
{
    public class ArchiveAdStatsCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public ArchiveAdStatsCacheCommand(long adId, DateTime caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
