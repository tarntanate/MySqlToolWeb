using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdGroupRedis.Commands.UpdateAdGroupStatsRedis;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.GetAdUnitByGroupIdCache
{
    public class GetAdUnitByGroupIdCacheQueryHandler : IRequestHandler<GetAdUnitByGroupIdCacheQuery, Response<string>>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public GetAdUnitByGroupIdCacheQueryHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<string>> Handle(GetAdUnitByGroupIdCacheQuery request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitListByPlatform(request.AdGroupId);
            var hashField = request.Platform.ToString();
            var redisValue = await AdsRedis.HashGetAsync(redisKey, hashField);

            var result = new Response<string>();
            if (redisValue.HasValue)
                return result.OK((string)redisValue);
            return result.NotFound();
        }
    }
}
