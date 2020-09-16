using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.InitialAssetAdStatsById
{
    public class InitialAdStatsByIdCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public long AdId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public InitialAdStatsByIdCommand(long adUnitId, long adId, DateTimeOffset caculatedAt)
        {
            AdUnitId = adUnitId;
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
