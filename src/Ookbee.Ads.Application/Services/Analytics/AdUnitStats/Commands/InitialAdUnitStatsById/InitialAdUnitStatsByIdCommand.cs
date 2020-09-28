using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.InitialAdUnitStatsById
{
    public class InitialAdUnitStatsByIdCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public long AdUnitId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public InitialAdUnitStatsByIdCommand(long adGroupId, long adUnitId, DateTimeOffset caculatedAt)
        {
            AdGroupId = adGroupId;
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
