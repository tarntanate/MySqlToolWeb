using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.InitialAdStatsCache
{
    public class InitialAdStatsCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdStatsCacheCommand(long adUnitId, DateTime caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
