using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.CreateAdStats
{
    public class CreateAdStatsCommand : IRequest<Response<long>>
    {
        public long AdId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }
        public long Impression { get; set; }
        public long Quota { get; set; }
        public long Click { get; set; }

        public CreateAdStatsCommand(long adId, DateTimeOffset caculatedAt, long quota, long impression, long click)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
            Quota = quota;
            Impression = impression;
            Click = click;
        }
    }
}
