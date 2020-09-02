using MediatR;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatById
{
    public class GetAdGroupStatByIdQuery : IRequest<HttpResult<AdGroupStatsDto>>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdGroupStatByIdQuery(long adGroupId, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
