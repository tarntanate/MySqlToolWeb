using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.InitialAdCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Mapping;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.CreateAdUnitCacheGroupId
{
    public class CreateAdUnitCacheByGroupIdCommandHandler : IRequestHandler<CreateAdUnitCacheByGroupIdCommand>
    {
        private readonly IMapper Mapper;
        private readonly IMediator Mediator;
        private IDatabase AdsRedis { get; }

        public CreateAdUnitCacheByGroupIdCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUnitCacheByGroupIdCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            var adUnits = new List<AdUnitDto>();

            do
            {
                next = false;
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, request.AdGroupId), cancellationToken);
                if (getAdUnitList.Ok &&
                    getAdUnitList.Data.HasValue())
                {
                    var items = getAdUnitList.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new InitialAdCacheCommand(item.Id), cancellationToken);
                        adUnits.Add(item);
                    }
                    start += length;
                    next = items.Count() < length ? false : true;
                }
            }
            while (next);

            if (adUnits.HasValue())
            {
                foreach (var platform in EnumHelper.GetValues<Platform>())
                {
                    if (platform != Platform.Unknown)
                    {
                        var cacheValue = new List<AdUnitCacheDto>();
                        foreach (var adUnit in adUnits)
                        {
                            var temp = adUnit.Clone();
                            temp.AdNetwork.UnitIds = adUnit.AdNetwork.UnitIds
                                .Where(unitId => unitId.DeletedAt == null && unitId.Platform == platform).ToList();
                            var item = Mapper.Map<AdUnitCacheDto>(temp);
                            cacheValue.Add(item);
                        }
                        if (cacheValue.HasValue())
                        {
                            var obj = PrepareAnalytics(cacheValue, platform);
                            var redisKey = CacheKey.Units(request.AdGroupId);
                            var hashField = platform.ToString();
                            var hashValue = JsonHelper.Serialize(obj);
                            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
                        }
                    }
                }
            }

            return Unit.Value;
        }

        private IEnumerable<AdUnitCacheDto> PrepareAnalytics(IEnumerable<AdUnitCacheDto> adUnits, Platform platform)
        {
            if (adUnits.HasValue())
            {
                foreach (var adUnit in adUnits)
                {
                    if (adUnit.Analytics.HasValue())
                    {
                        var name = "platform";
                        var value = platform.ToString();

                        if (adUnit.Analytics.Clicks.HasValue())
                            adUnit.Analytics.Clicks = adUnit.Analytics.Clicks.Select(url =>
                            {
                                url = new Uri(url).AddQueryString(name, value).AbsoluteUri;
                                return url.ToLower();
                            }).ToList();

                        if (adUnit.Analytics.Impressions.HasValue())
                            adUnit.Analytics.Impressions = adUnit.Analytics.Impressions.Select(url =>
                            {
                                url = new Uri(url).AddQueryString(name, value).AbsoluteUri;
                                return url.ToLower();
                            }).ToList();
                    }
                }
            }

            return adUnits;
        }
    }
}
