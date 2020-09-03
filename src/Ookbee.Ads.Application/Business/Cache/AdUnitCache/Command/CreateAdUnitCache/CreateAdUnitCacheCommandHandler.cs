using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.CreateAdAssetCache;
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

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.CreateAdUnitCache
{
    public class CreateAdUnitCacheCommandHandler : IRequestHandler<CreateAdUnitCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdUnitCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUnitCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            var adUnits = new List<AdUnitCacheDto>();
            do
            {
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, request.AdGroupId), cancellationToken);
                if (getAdUnitList.Ok)
                {
                    var units = getAdUnitList.Data;
                    foreach (var unit in units)
                    {
                        await CreateAdAssetCache(unit.Id, cancellationToken);
                        var adUnit = Mapper.Map<AdUnitCacheDto>(unit);
                        adUnits.Add(adUnit);
                    }
                }
                next = getAdUnitList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            if (adUnits.HasValue())
            {
                foreach (Platform platform in Enum.GetValues(typeof(Platform)))
                {
                    var platformName = platform.ToString();
                    foreach (var adUnit in adUnits)
                    {
                        if (adUnit.Analytics.HasValue())
                        {
                            var queryName = "platform";
                            var queryValue = platformName.ToLower();

                            if (adUnit.Analytics.Clicks.HasValue())
                                adUnit.Analytics.Clicks = adUnit.Analytics.Clicks.Select(url =>
                                {
                                    url = new Uri(url).AddQueryString(queryName, queryValue).AbsoluteUri;
                                    return url;
                                }).ToList();

                            if (adUnit.Analytics.Impressions.HasValue())
                                adUnit.Analytics.Impressions = adUnit.Analytics.Impressions.Select(url =>
                                {
                                    url = new Uri(url).AddQueryString(queryName, queryValue).AbsoluteUri;
                                    return url;
                                }).ToList();
                        }
                    }

                    var redisKey = CacheKey.Units(request.AdGroupId, platform);
                    var redisValue = JsonHelper.Serialize(adUnits);
                    await AdsRedis.StringSetAsync(redisKey, redisValue);
                }
            }

            return Unit.Value;
        }

        public async Task CreateAdAssetCache(long adUnitId, CancellationToken cancellationToken)
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
                        await Mediator.Send(new CreateAdAssetCacheCommand(ad.Id), cancellationToken);
                    }
                    start += length;
                }
                next = getAdList.Data.Count() < length ? false : true;
            }
            while (next);
        }
    }
}
