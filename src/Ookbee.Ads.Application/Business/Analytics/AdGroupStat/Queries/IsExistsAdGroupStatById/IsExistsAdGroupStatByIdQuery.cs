using System;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.IsExistsAdGroupStatById
{
    public class IsExistsAdGroupStatByIdQuery : IRequest<HttpResult<bool>>
    {
        public long AdGroupId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public IsExistsAdGroupStatByIdQuery(long adGroupId, DateTime caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
