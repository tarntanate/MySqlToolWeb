﻿using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitById;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.DeleteAdCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.DeleteAdUnitCache
{
    public class DeleteAdUnitCacheCommandHandler : IRequestHandler<DeleteAdUnitCacheCommand>
    {
        private IMediator Mediator { get; }
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
            if (getAdUnitById.Ok)
            {
                var adUnit = getAdUnitById.Data;
                foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
                {
                    if (platform != Platform.Unknown)
                    {
                        var platformName = platform.ToString();
                        var redisKey = CacheKey.Units(adUnit.AdGroup.Id, platform);
                        var redisValue = await AdsRedis.StringGetAsync(redisKey);
                        if (redisValue.HasValue())
                        {
                            var adUnits = JsonHelper.Deserialize<List<AdUnitCacheDto>>(redisValue);
                            var index = adUnits.FindIndex(x => x.Name == adUnit.AdNetwork);
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
                var getAdList = await Mediator.Send(new GetAdListQuery(start, length, adUnitId, null), cancellationToken);
                if (getAdList.Ok)
                {
                    foreach (var ad in getAdList.Data)
                    {
                        await Mediator.Send(new DeleteAdCacheCommand(ad.Id), cancellationToken);
                    }
                    start += length;
                }
                next = getAdList.Data.Count() < length ? false : true;
            }
            while (next);
        }
    }
}
