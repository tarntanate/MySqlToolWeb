using MediatR;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAdGroupIdCache
{
    public class CreateAdGroupCacheCommandHandler : IRequestHandler<CreateAdGroupIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public CreateAdGroupCacheCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupIdCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupIdList();
            var redisValues = request.AdGroupIds.Select(x => (RedisValue)x).ToArray();
            await AdsRedis.SetAddAsync(redisKey, redisValues, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
