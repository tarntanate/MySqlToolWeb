using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.InitialAdGroupStatsById
{
    public class InitialAdGroupStatsByIdCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdGroupStatsByIdCommand(long adGroupId, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
