using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUserCache.Queries.IsExistsAdUserCacheById
{
    public class GetAdUserCacheByIdQueryHandler : IRequestHandler<IsExistsAdUserCacheByIdQuery, Response<bool>>
    {
        private readonly IDatabase AdsRedis;

        public GetAdUserCacheByIdQueryHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<bool>> Handle(IsExistsAdUserCacheByIdQuery request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UserPreview();
            var redisValue = request.UserId;
            var isExists = await AdsRedis.SetContainsAsync(redisKey, redisValue);

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
