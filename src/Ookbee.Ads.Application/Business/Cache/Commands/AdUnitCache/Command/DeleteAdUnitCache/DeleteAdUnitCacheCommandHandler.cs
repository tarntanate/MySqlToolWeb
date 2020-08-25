using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.DeleteAdAssetCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.DeleteAdUnitCache
{
    public class DeleteAdUnitCacheCommandHandler : IRequestHandler<DeleteAdUnitCacheCommand, Unit>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public DeleteAdUnitCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(DeleteAdUnitCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitsByGroup(request.AdGroupId);
            var redisValue = await AdsRedis.StringGetAsync(redisKey);
            if (redisValue.HasValue())
            {
                var adUnits = JsonHelper.Deserialize<List<AdUnitCacheDto>>(redisValue);
                var index = adUnits.FindIndex(x => x.Name == request.Unit);
                if (index > -1)
                {
                    adUnits.RemoveAt(index);
                }

                if (adUnits.HasValue())
                {
                    redisValue = JsonHelper.Serialize(adUnits);
                    await AdsRedis.StringSetAsync(redisKey, redisValue);
                }
                else
                {
                    await AdsRedis.KeyDeleteAsync(redisKey);
                }
            }

            return Unit.Value;
        }

        public async Task DeleteAdAssetCache(long adUnitId, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdList = await Mediator.Send(new GetAdListQuery(start, length, adUnitId, null), cancellationToken);
                if (getAdList.Ok)
                {
                    foreach (var ad in getAdList.Data)
                    {
                        await Mediator.Send(new DeleteAdAssetCacheCommand(ad.Id), cancellationToken);
                    }
                    start += length;
                }
                next = getAdList.Data.Count() < length ? false : true;
            }
            while (next);
        }
    }
}
