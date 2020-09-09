using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.InitialAssetAdStats
{
    public class InitialAdStatsCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdStatsCommand(long adId, DateTime caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
