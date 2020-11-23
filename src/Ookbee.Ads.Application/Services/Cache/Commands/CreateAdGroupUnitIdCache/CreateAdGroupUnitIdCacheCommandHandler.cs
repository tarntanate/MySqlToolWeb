using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAdGroupUnitIdCache
{
    public class CreateAdGroupUnitIdCacheCommandHandler : IRequestHandler<CreateAdGroupUnitIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public CreateAdGroupUnitIdCacheCommandHandler(
            AdsRedisContext adsRedis,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdsRedis = adsRedis.Database();
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Unit> Handle(CreateAdGroupUnitIdCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdGroupUnitId(request.AdGroupId);
            var redisValues = request.AdUnitIds.Select(x => (RedisValue)x).ToArray();
            await AdsRedis.SetAddAsync(redisKey, redisValues, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
