using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdStats.Commands.UpdateAdStats
{
    public class UpdateAdStatsCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }
        public Platform Platform { get; set; }
        public long Impression { get; set; }
        public long Click { get; set; }

        public UpdateAdStatsCommand(long id, long adGroupId, DateTime caculatedAt, Platform platform, long impression, long click)
        {
            Id = id;
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
            Platform = platform;
            Impression = impression;
            Click = click;
        }
    }
}
