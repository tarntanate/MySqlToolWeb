using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAdGroupIdCache
{
    public class CreateAdGroupRedisCommandHandler : IRequestHandler<CreateAdGroupIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public CreateAdGroupRedisCommandHandler(
            AdsDbRepository<AdGroupEntity> adGroupDbRepo,
            AdsRedisContext adsRedis)
        {
            AdGroupDbRepo = adGroupDbRepo;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupIdCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var items = await AdGroupDbRepo.FindAsync(
                    filter: f => f.DeletedAt == null,
                    selector: f => new { f.Id },
                    start: start,
                    length: length
                );
                if (items.HasValue())
                {
                    var redisKey = CacheKey.GroupIdList();
                    var redisValues = items.Select(x => (RedisValue)x.Id).ToArray();
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
