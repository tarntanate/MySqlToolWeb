using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.InitialAdAssetStatsCache
{
    public class InitialAdAssetStatsCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdAssetStatsCacheCommand(long adUnitId, DateTime caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
