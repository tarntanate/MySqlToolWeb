using MediatR;
using Ookbee.Ads.Application.Services.Cache.Queries.GetAdUnitIdListCache;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdUnitIdCache
{
    public class DeleteAdUnitIdCacheCommandHandler : IRequestHandler<DeleteAdUnitIdCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public DeleteAdUnitIdCacheCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdUnitIdCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdUnitIds = await Mediator.Send(new GetAdUnitIdListCacheQuery(), cancellationToken);
            if (getAdUnitIds.IsSuccess)
            {
                foreach (var adUnitId in getAdUnitIds.Data)
                {
                    var predicate = AdUnitCacheFilter.Available(adUnitId);
                    var item = await AdUnitDbRepo.FirstAsync(
                        filter: predicate,
                        selector: f => new { f.Id, f.AdGroupId }
                    );
                    if (!item.HasValue())
                    {
                        var redisKey = CacheKey.UnitIdList();
                        var redisValue = (RedisValue)item.Id;
                        await AdsRedis.SetRemoveAsync(redisKey, redisValue, CommandFlags.FireAndForget);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
