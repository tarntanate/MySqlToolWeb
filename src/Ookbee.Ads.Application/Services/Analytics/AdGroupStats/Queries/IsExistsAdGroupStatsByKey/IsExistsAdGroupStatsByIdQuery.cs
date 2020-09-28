using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.IsExistsAdGroupStatsByKey
{
    public class IsExistsAdGroupStatsByKeyQuery : IRequest<Response<bool>>
    {
        public long AdGroupId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public IsExistsAdGroupStatsByKeyQuery(long adGroupId, DateTimeOffset caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
