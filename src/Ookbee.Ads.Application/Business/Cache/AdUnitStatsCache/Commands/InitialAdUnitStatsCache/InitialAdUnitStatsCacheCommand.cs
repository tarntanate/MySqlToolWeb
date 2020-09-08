using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.InitialAdUnitStatsCache
{
    public class InitialAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdUnitStatsCacheCommand(long adGroupId, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
