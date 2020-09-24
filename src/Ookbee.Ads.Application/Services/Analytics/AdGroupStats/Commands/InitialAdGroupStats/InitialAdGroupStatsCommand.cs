using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStatsCache.Commands.InitialAdGroupStats
{
    public class InitialAdGroupStatsCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }

        public InitialAdGroupStatsCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
