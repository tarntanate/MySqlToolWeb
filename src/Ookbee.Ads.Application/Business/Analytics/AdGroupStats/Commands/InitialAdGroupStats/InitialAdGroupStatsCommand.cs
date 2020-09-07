﻿using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.InitialAdGroupStats
{
    public class InitialAdGroupStatsCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdGroupStatsCommand(long adGroupId, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}