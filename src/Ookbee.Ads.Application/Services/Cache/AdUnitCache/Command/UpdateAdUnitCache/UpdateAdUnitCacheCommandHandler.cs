using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitList;
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

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.UpdateAdUnitCache
{
    public class UpdateAdUnitCacheCommandHandler : IRequestHandler<UpdateAdUnitCacheCommand>
    {
        private readonly IMapper Mapper;
        private readonly IMediator Mediator;
        private IDatabase AdsRedis { get; }

        public UpdateAdUnitCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateAdUnitCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            var adUnits = new List<AdUnitCacheDto>();
            do
            {
                next = false;
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, request.AdGroupId), cancellationToken);
                if (getAdUnitList.Ok &&
                    getAdUnitList.HasValue())
                {
                    var items = getAdUnitList.Data;
                    foreach (var item in items)
                    {
                        var adUnit = Mapper.Map<AdUnitCacheDto>(item);
                        adUnits.Add(adUnit);
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
                        var platformName = platform.ToString();
                        foreach (var group in adUnits)
                        {
                            if (group.Analytics.HasValue())
                            {
                                var queryName = "platform";
                                var queryValue = platformName.ToLower();

                                if (group.Analytics.Clicks.HasValue())
                                    group.Analytics.Clicks = group.Analytics.Clicks.Select(url =>
                                    {
                                        url = new Uri(url).AddQueryString(queryName, queryValue).AbsoluteUri;
                                        return url;
                                    }).ToList();

                                if (group.Analytics.Impressions.HasValue())
                                    group.Analytics.Impressions = group.Analytics.Impressions.Select(url =>
                                    {
                                        url = new Uri(url).AddQueryString(queryName, queryValue).AbsoluteUri;
                                        return url;
                                    }).ToList();
                            }
                        }
                        var redisKey = CacheKey.Units(request.AdGroupId);
                        var hashField = platformName;
                        var hashValue = JsonHelper.Serialize(adUnits);
                        await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
