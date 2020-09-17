using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.InitialAdUnitStatsById
{
    public class InitialAdUnitStatsByIdCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdUnitStatsByIdCommand(long adUnitId, DateTime caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
