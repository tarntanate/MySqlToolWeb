using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.InitialAssetAdStatsById
{
    public class InitialAdStatsByIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public InitialAdStatsByIdCommand(long adId, DateTime caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
