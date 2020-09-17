using MediatR;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsByKey
{
    public class GetAdGroupStatsByKeyQuery : IRequest<HttpResult<AdGroupStatsDto>>
    {
        public long AdGroupId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public GetAdGroupStatsByKeyQuery(long adGroupId, DateTimeOffset caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
