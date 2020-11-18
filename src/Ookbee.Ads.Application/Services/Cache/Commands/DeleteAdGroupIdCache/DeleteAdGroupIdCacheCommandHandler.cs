using MediatR;
using Ookbee.Ads.Application.Services.Cache.Queries.GetAdGroupIdListCache;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupIdCache
{
    public class DeleteAdGroupIdCacheCommandHandler : IRequestHandler<DeleteAdGroupIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public DeleteAdGroupIdCacheCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdGroupIdCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupIdList();
            var redisValue = (RedisValue)request.AdGroupId;
            await AdsRedis.SetRemoveAsync(redisKey, redisValue, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
