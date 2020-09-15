using MediatR;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdQuotaById
{
    public class GetAdQuotaByIdQuery : IRequest<HttpResult<int>>
    {
        public long AdId { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdQuotaByIdQuery(long adId, DateTime caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
