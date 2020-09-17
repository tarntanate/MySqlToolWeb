using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStatsCache.Commands.InitialAdUnitStats
{
    public class InitialAdUnitStatsCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdGroupId { get; set; }

        public InitialAdUnitStatsCommand(DateTime caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
