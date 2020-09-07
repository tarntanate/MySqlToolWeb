using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatsCommand : IRequest<HttpResult<long>>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }
        public long Request { get; set; }

        public CreateAdGroupStatsCommand(long adGroupId, Platform platform, DateTime caculatedAt, long request)
        {
            AdGroupId = adGroupId;
            Platform = platform;
            CaculatedAt = caculatedAt;
            Request = request;
        }
    }
}
