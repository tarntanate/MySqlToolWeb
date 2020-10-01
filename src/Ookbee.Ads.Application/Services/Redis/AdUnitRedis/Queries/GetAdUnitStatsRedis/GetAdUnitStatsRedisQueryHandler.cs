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

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdUnitStatsRedis
{
    public class GetAdUnitStatsRedisQueryHandler : IRequestHandler<GetAdUnitStatsRedisQuery, Response<Dictionary<AdStatsType, long>>>
    {
        private readonly IDatabase AdsRedis;

        public GetAdUnitStatsRedisQueryHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<Dictionary<AdStatsType, long>>> Handle(GetAdUnitStatsRedisQuery request, CancellationToken cancellationToken)
        {
            var result = new Response<Dictionary<AdStatsType, long>>();
            var redisKey = CacheKey.UnitStats(request.AdUnitId);
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
