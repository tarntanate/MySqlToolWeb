using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdGroupCache.Queries.GetAdGroupIdListCache;
using Ookbee.Ads.Application.Services.Cache.AdUnitCache.Queries.GetAdUnitIdListCache;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.DeleteAdUnitIdByGroupIdCache
{
    public class DeleteAdUnitIdByGroupIdCacheCommandHandler : IRequestHandler<DeleteAdUnitIdByGroupIdCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public DeleteAdUnitIdByGroupIdCacheCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdUnitIdByGroupIdCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdUnitIds = await Mediator.Send(new GetAdUnitIdListCacheQuery(), cancellationToken);
            if (getAdUnitIds.IsSuccess)
            {
                foreach (var adUnitId in getAdUnitIds.Data)
                {
                    var item = await AdUnitDbRepo.FirstAsync(
                        filter: AdUnitFilter.Available(),
                        selector: f => new { f.Id }
                    );
                    if (!item.HasValue())
                    {
                        var getAdGroupIds = await Mediator.Send(new GetAdGroupIdListCacheQuery(), cancellationToken);
                        if (getAdGroupIds.IsSuccess)
                        {
                            foreach (var adGroupId in getAdGroupIds.Data)
                            {
                                var groupUnitIdListKey = AdUnitKey.Ids(adGroupId);
                                var groupUnitIdListvalue = (RedisValue)item.Id;
                                await AdsRedis.SetRemoveAsync(groupUnitIdListKey, groupUnitIdListvalue, CommandFlags.FireAndForget);
                            }
                        }
                    }
                }
            }

            return Unit.Value;
        }
    }
}
