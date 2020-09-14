using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStatsCache.Commands.InitialAdStats
{
    public class InitialAdStatsCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdStatsCommand(long adUnitId, DateTime caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
