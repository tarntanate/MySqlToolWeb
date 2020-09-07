using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Queries.GetAdUnitStatsByKey
{
    public class GetAdUnitStatsByKeyQuery : IRequest<HttpResult<AdUnitStatsDto>>
    {
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdUnitStatsByKeyQuery(long adUnitId, Platform platform, DateTime caculatedAt)
        {
            AdUnitId = adUnitId;
            Platform = platform;
            CaculatedAt = caculatedAt;
        }
    }
}
