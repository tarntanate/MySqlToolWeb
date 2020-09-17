using MediatR;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Queries.GetAdUnitStatsByKey
{
    public class GetAdUnitStatsByKeyQuery : IRequest<HttpResult<AdUnitStatsDto>>
    {
        public long AdUnitId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdUnitStatsByKeyQuery(long adUnitId, DateTime caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
