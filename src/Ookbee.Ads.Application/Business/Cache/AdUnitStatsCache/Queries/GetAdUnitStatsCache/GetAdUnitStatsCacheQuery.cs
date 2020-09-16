using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.GetAdUnitStatsCache
{
    public class GetAdUnitStatsCacheQuery : IRequest<HttpResult<Dictionary<string, long>>>
    {
        public long AdUnitId { get; set; }

        public GetAdUnitStatsCacheQuery(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
