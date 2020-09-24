using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupStatsCache.Commands.GetAdGroupStatsCache
{
    public class GetAdGroupStatsCacheQueryHandler : IRequestHandler<GetAdGroupStatsCacheQuery, Response<Dictionary<string, long>>>
    {
        private readonly IDatabase AdsRedis;

        public GetAdGroupStatsCacheQueryHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<Dictionary<string, long>>> Handle(GetAdGroupStatsCacheQuery request, CancellationToken cancellationToken)
        {
            var result = new Response<Dictionary<string, long>>();

            var redisKey = CacheKey.GroupStats(request.AdGroupId);
            var hashEntries = await AdsRedis.HashGetAllAsync(redisKey);
            if (hashEntries.HasValue())
            {
                var data = hashEntries.ToDictionary(x => x.Name.ToString(), x => (long)x.Value, StringComparer.Ordinal);
                return result.Success(data);
            }
            return result.Fail(404, "Data not found.");
        }
    }
}
