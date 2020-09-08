using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.ArchiveAdAssetStatsByIdCache
{
    public class ArchiveAdAssetStatsByIdCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdId { get; set; }

        public ArchiveAdAssetStatsByIdCacheCommand(DateTime caculatedAt, long adId)
        {
            CaculatedAt = caculatedAt;
            AdId = adId;
        }
    }
}
