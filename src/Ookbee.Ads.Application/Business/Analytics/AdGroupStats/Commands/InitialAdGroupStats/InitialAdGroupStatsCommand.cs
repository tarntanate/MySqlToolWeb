using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStatsCache.Commands.InitialAdGroupStats
{
    public class InitialAdGroupStatsCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }

        public InitialAdGroupStatsCommand(DateTime caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
