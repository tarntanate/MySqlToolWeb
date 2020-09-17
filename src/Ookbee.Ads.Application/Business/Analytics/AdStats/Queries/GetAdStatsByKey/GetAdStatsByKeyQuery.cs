using MediatR;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdStatsByKey
{
    public class GetAdStatsByKeyQuery : IRequest<HttpResult<AdStatsDto>>
    {
        public long AdId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdStatsByKeyQuery(long adId, DateTime caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
