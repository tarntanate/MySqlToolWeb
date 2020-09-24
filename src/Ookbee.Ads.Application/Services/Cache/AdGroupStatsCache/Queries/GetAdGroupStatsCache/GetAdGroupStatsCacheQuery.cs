using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupStatsCache.Commands.GetAdGroupStatsCache
{
    public class GetAdGroupStatsCacheQuery : IRequest<Response<Dictionary<string, long>>>
    {
        public long AdGroupId { get; set; }

        public GetAdGroupStatsCacheQuery(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
