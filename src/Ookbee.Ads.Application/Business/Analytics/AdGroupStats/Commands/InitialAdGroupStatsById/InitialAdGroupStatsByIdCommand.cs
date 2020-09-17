using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.InitialAdGroupStatsById
{
    public class InitialAdGroupStatsByIdCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdGroupId { get; set; }

        public InitialAdGroupStatsByIdCommand(DateTime caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
