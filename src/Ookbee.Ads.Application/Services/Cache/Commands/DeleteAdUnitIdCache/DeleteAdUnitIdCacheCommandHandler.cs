using MediatR;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdUnitIdCache
{
    public class DeleteAdUnitIdCacheCommandHandler : IRequestHandler<DeleteAdUnitIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public DeleteAdUnitIdCacheCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdUnitIdCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitIdList();
            var redisValue = (RedisValue)request.AdUnitId;
            await AdsRedis.SetRemoveAsync(redisKey, redisValue, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
