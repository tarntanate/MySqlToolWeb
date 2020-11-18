using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAdUnitIdCache
{
    public class CreateAdUnitRedisCommandHandler : IRequestHandler<CreateAdUnitIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public CreateAdUnitRedisCommandHandler(
            AdsRedisContext adsRedis,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdsRedis = adsRedis.Database();
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Unit> Handle(CreateAdUnitIdCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var predicate = AdUnitCacheFilter.Available();
                var items = await AdUnitDbRepo.FindAsync(
                    filter: predicate,
                    selector: f => new { f.Id },
                    start: start,
                    length: length
                );
                if (items.HasValue())
                {
                    var redisKey = CacheKey.UnitIdList();
                    var redisValues = items.Select(item => (RedisValue)item.Id).ToArray();
                    await AdsRedis.SetAddAsync(redisKey, redisValues, CommandFlags.FireAndForget);

                    next = items.Count() == length ? true : false;
                    start += length;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
