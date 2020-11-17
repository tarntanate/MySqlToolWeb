using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.CreateAdUnitIdByGroupIdCache
{
    public class CreateAdUnitIdByGroupIdCacheCommandHandler : IRequestHandler<CreateAdUnitIdByGroupIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public CreateAdUnitIdByGroupIdCacheCommandHandler(
            AdsRedisContext adsRedis,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdsRedis = adsRedis.Database();
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Unit> Handle(CreateAdUnitIdByGroupIdCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var items = await AdUnitDbRepo.FindAsync(
                    filter: AdUnitFilter.Available(),
                    selector: f => new { f.Id, f.AdGroupId },
                    start: start,
                    length: length
                );
                if (items.HasValue())
                {
                    var adGroupIds = items.Select(item => item.AdGroupId).Distinct();
                    foreach (var adGroupId in adGroupIds)
                    {
                        var key = CacheKey.UnitIdListByGroupId(adGroupId);
                        var values = items.Where(item => item.AdGroupId == adGroupId).Select(item => (RedisValue)item.Id).ToArray();
                        await AdsRedis.SetAddAsync(key, values, CommandFlags.FireAndForget);
                    }

                    next = items.Count() == length ? true : false;
                    start += length;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
