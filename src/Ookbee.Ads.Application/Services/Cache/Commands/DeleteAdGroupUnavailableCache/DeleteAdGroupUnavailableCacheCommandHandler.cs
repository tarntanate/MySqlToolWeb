using MediatR;
using Ookbee.Ads.Application.Services.Cache.Queries.GetAdGroupIdListCache;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupUnavailableCache
{
    public class DeleteAdGroupUnavailableCacheCommandHandler : IRequestHandler<DeleteAdGroupUnavailableCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public DeleteAdGroupUnavailableCacheCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdGroupUnavailableCacheCommand request, CancellationToken cancellationToken)
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
                        //await Mediator.Send(new DeleteAdGroupIdCacheCommand(adGroupId), cancellationToken);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
