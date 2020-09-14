using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStatsCache.Commands.InitialAdUnitStats
{
    public class InitialAdUnitStatsCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdUnitStatsCommand(long adGroupId, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
