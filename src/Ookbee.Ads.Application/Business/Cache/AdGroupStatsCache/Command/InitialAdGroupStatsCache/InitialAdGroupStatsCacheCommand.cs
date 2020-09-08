using System;
using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.InitialAdGroupStatsCache
{
    public class InitialAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }

        public InitialAdGroupStatsCacheCommand(DateTime caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
