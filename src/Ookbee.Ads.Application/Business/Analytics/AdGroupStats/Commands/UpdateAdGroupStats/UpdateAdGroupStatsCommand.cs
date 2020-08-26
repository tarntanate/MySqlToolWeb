using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStats.Commands.UpdateAdGroupStats
{
    public class UpdateAdGroupStatsCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }

        public UpdateAdGroupStatsCommand(long id, long adGroupId, DateTime caculatedAt, Platform platform, long request)
        {
            Id = id;
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
            Platform = platform;
            Request = request;
        }
    }
}
