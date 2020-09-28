using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsByKey
{
    public class GetAdGroupStatsByKeyQuery : IRequest<Response<AdGroupStatsDto>>
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
