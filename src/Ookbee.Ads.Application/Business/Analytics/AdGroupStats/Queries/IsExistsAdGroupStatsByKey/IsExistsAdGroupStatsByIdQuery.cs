using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.IsExistsAdGroupStatsByKey
{
    public class IsExistsAdGroupStatsByKeyQuery : IRequest<HttpResult<bool>>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }

        public IsExistsAdGroupStatsByKeyQuery(long adGroupId, Platform platform, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            Platform = platform;
            CaculatedAt = caculatedAt;
        }
    }
}
