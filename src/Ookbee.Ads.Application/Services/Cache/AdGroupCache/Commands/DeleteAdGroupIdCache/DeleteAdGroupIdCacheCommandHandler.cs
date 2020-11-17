using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdGroupCache.Queries.GetAdGroupIdListCache;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupCache.Commands.DeleteAdGroupIdCache
{
    public class DeleteAdGroupIdCacheCommandHandler : IRequestHandler<DeleteAdGroupIdCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public DeleteAdGroupIdCacheCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdGroupIdCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupIds = await Mediator.Send(new GetAdGroupIdListCacheQuery(), cancellationToken);
            if (getAdGroupIds.IsSuccess)
            {
                foreach (var adGroupId in getAdGroupIds.Data)
                {
                    var isExists = await AdGroupDbRepo.AnyAsync(
                        filter: f => f.DeletedAt == null
                    );
                    if (!isExists)
                    {
                        var redisKey = CacheKey.GroupIdList();
                        var redisValue = (RedisValue)adGroupId;
                        await AdsRedis.SetRemoveAsync(redisKey, redisValue, CommandFlags.FireAndForget);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
