using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitById;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.DeleteAdCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.DeleteAdUnitCache
{
    public class DeleteAdUnitCacheCommandHandler : IRequestHandler<DeleteAdUnitCacheCommand>
    {
        private readonly IMediator Mediator;
        private IDatabase AdsRedis { get; }

        public DeleteAdUnitCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdUnitCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdUnitById = await Mediator.Send(new GetAdUnitByIdQuery(request.AdUnitId), cancellationToken);
            if (getAdUnitById.Ok &&
                getAdUnitById.Data.HasValue())
            {
                var adUnit = getAdUnitById.Data;
                foreach (var platform in EnumHelper.GetValues<AdPlatform>())
                {
                    if (platform != AdPlatform.Unknown)
                    {
                        var platformName = platform.ToString();
                        var redisKey = CacheKey.Units(adUnit.AdGroup.Id);
                        var hashField = platform.ToString();
                        var hashValue = await AdsRedis.HashGetAsync(redisKey, hashField);
                        if (hashValue.HasValue())
                        {
                            var adUnits = JsonHelper.Deserialize<List<AdUnitCacheDto>>(hashValue);
                            var index = adUnits.FindIndex(x => x.Name == adUnit.AdNetwork.Name);
                            if (index > -1)
                            {
                                adUnits.RemoveAt(index);
                            }

                            if (adUnits.HasValue())
                            {
                                hashValue = JsonHelper.Serialize(adUnits);
                                await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
                            }
                            else
                            {
                                await AdsRedis.HashDeleteAsync(redisKey, hashField);
                            }
                        }
                    }
                }
                await DeleteAdCache(adUnit.Id, cancellationToken);
            }

            return Unit.Value;
        }

        public async Task DeleteAdCache(long adUnitId, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                next = false;
                var getAdListRequest = new GetAdListQuery(start, length, adUnitId, null);
                var getAdListResponse = await Mediator.Send(getAdListRequest, cancellationToken);
                if (getAdListResponse.Data.HasValue())
                {
                    var items = getAdListResponse.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new DeleteAdCacheCommand(item.Id), cancellationToken);
                    }
                    start += length;
                    next = items.Count() < length ? false : true;
                }
            }
            while (next);
        }
    }
}
