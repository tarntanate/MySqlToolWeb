using System;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatsCommand : IRequest<HttpResult<long>>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }
        public DateTime CaculatedAt { get; set; }

        public CreateAdGroupStatsCommand(long adGroupId, Platform platform, long request, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            Platform = platform;
            Request = request;
            CaculatedAt = caculatedAt;
        }
    }
}
