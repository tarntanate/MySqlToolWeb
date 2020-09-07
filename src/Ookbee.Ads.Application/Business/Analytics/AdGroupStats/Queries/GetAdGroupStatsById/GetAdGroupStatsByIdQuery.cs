using MediatR;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsById
{
    public class GetAdGroupStatsByIdQuery : IRequest<HttpResult<AdGroupStatsDto>>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdGroupStatsByIdQuery(long adGroupId, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
