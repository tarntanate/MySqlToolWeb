using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsByKey
{
    public class GetAdGroupStatsByKeyQuery : IRequest<HttpResult<AdGroupStatsDto>>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdGroupStatsByKeyQuery(long adGroupId, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
