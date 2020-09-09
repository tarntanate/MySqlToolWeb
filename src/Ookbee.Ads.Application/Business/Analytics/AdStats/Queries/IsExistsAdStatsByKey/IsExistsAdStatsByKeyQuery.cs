using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.IsExistsAStatsByKey
{
    public class IsExistsAdStatsByKeyQuery : IRequest<HttpResult<bool>>
    {
        public long AdId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }

        public IsExistsAdStatsByKeyQuery(long adId, Platform platform, DateTime caculatedAt)
        {
            AdId = adId;
            Platform = platform;
            CaculatedAt = caculatedAt;
        }
    }
}
