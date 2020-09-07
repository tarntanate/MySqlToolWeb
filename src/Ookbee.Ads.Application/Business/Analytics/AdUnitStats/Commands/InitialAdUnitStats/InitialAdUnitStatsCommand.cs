using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.InitialAdUnitStats
{
    public class InitialAdUnitStatsCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdUnitStatsCommand(long adUnitId, DateTime caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
