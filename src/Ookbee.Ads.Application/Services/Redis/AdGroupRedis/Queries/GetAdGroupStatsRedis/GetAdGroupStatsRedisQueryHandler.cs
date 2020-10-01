using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupStatsRedis
{
    public class GetAdGroupStatsRedisQueryHandler : IRequestHandler<GetAdGroupStatsRedisQuery, Response<Dictionary<AdStatsType, long>>>
    {
        private readonly IDatabase AdsRedis;

        public GetAdGroupStatsRedisQueryHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<Dictionary<AdStatsType, long>>> Handle(GetAdGroupStatsRedisQuery request, CancellationToken cancellationToken)
        {
            var result = new Response<Dictionary<AdStatsType, long>>();
            var redisKey = CacheKey.GroupStats(request.AdGroupId);
            var redisValues = await AdsRedis.HashGetAllAsync(redisKey);
            if (redisValues.HasValue())
            {
                var items = redisValues.ToDictionary(v => EnumHelper.ConvertTo<AdStatsType>(v.Name), v => (long)v.Value);
                return result.OK(items);
            }
            return result.NotFound();
        }
    }
}
