using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Commands.CreateAdStats
{
    public class CreateAdStatsCommand : IRequest<HttpResult<long>>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }
        public Platform Platform { get; set; }
        public long Impression { get; set; }
        public long Click { get; set; }

        public CreateAdStatsCommand(long adGroupId, DateTime caculatedAt, Platform platform, long impression, long click)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
            Platform = platform;
            Impression = impression;
            Click = click;
        }
    }
}
