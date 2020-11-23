using MediatR;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAdUnitIdCache
{
    public class CreateAdUnitIdCacheCommandHandler : IRequestHandler<CreateAdUnitIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public CreateAdUnitIdCacheCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUnitIdCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitIdList();
            var redisValues = request.AdUnitIds.Select(x => (RedisValue)x).ToArray();
            await AdsRedis.SetAddAsync(redisKey, redisValues, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
