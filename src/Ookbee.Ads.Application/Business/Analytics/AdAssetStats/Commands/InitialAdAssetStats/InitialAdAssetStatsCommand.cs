using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.InitialAssetAdStats
{
    public class InitialAdAssetStatsCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdAssetStatsCommand(long adId, DateTime caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
