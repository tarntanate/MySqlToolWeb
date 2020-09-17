using MediatR;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.CreateAdStats
{
    public class CreateAdStatsCommand : IRequest<HttpResult<long>>
    {
        public long AdId { get; set; }
        public DateTime CaculatedAt { get; set; }
        public long Impression { get; set; }
        public long Quota { get; set; }
        public long Click { get; set; }

        public CreateAdStatsCommand(long adId, DateTime caculatedAt, long quota, long impression, long click)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
            Quota = quota;
            Impression = impression;
            Click = click;
        }
    }
}
