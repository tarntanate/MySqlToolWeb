using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdUserRedis.Commands.IsExistsAdUserPreviewRedis
{
    public class IsExistsAdUserPreviewRedisQueryHandler : IRequestHandler<IsExistsAdUserPreviewRedisQuery, Response<bool>>
    {
        private readonly IDatabase AdsRedis;

        public IsExistsAdUserPreviewRedisQueryHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<bool>> Handle(IsExistsAdUserPreviewRedisQuery request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UserIdsPreview();
            var redisValue = request.UserId;
            var isExists = await AdsRedis.SetContainsAsync(redisKey, redisValue);

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
