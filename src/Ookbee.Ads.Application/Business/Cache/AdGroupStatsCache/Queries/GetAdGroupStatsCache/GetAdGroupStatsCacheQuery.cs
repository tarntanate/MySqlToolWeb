using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.GetAdGroupStatsCache
{
    public class GetAdGroupStatsCacheQuery : IRequest<HttpResult<Dictionary<string, long>>>
    {
        public long AdGroupId { get; set; }

        public GetAdGroupStatsCacheQuery(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
