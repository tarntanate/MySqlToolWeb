using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStatsCache.Commands.InitialAdUnitStats
{
    public class InitialAdUnitStatsCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdGroupId { get; set; }

        public InitialAdUnitStatsCommand(DateTimeOffset caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
