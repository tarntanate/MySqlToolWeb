using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.CreateAdAssetStats
{
    public class CreateAdAssetStatsCommand : IRequest<HttpResult<long>>
    {
        public long AdId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }
        public long Impression { get; set; }
        public long Click { get; set; }

        public CreateAdAssetStatsCommand(long adId, Platform platform, DateTime caculatedAt, long impression, long click)
        {
            AdId = adId;
            Platform = platform;
            CaculatedAt = caculatedAt;
            Impression = impression;
            Click = click;
        }
    }
}
