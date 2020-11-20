using MediatR;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupUnitIdCache
{
    public class DeleteAdGroupUnitIdCacheCommandHandler : IRequestHandler<DeleteAdGroupUnitIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public DeleteAdGroupUnitIdCacheCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdGroupUnitIdCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdGroupUnitId(request.AdGroupId);
            var redisValue = (RedisValue)request.AdUnitId;
            await AdsRedis.KeyDeleteAsync(redisKey, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
