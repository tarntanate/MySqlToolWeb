using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatsCommand : IRequest<HttpResult<long>>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }

        public CreateAdGroupStatsCommand(long adGroupId, DateTime caculatedAt, Platform platform, long request)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
            Platform = platform;
            Request = request;
        }
    }
}
