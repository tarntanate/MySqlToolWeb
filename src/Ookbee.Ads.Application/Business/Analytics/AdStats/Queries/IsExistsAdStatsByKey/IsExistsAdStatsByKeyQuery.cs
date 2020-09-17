using MediatR;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.IsExistsAStatsByKey
{
    public class IsExistsAdStatsByKeyQuery : IRequest<HttpResult<bool>>
    {
        public long AdId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public IsExistsAdStatsByKeyQuery(long adId, DateTime caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
