using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdStatsCache.Commands.InitialAdStats
{
    public class InitialAdStatsCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public InitialAdStatsCommand(long adUnitId, DateTimeOffset caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
