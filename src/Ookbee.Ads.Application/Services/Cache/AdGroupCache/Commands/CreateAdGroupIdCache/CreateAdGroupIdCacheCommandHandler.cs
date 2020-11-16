using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.CacheManager.AdGroupCache.Commands.CreateAdGroupIdCache
{
    public class CreateAdGroupRedisCommandHandler : IRequestHandler<CreateAdGroupIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public CreateAdGroupRedisCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupIdCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupIds();
            var redisValue = (RedisValue)request.AdGroupId;
            await AdsRedis.SetAddAsync(redisKey, redisValue, CommandFlags.FireAndForget);
                        
            return Unit.Value;
        }
    }
}
