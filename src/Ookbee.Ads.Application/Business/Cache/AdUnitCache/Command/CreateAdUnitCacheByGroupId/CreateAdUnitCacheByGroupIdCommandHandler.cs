using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.InitialAdCache;
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

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.CreateAdUnitCacheGroupId
{
    public class CreateAdUnitCacheByGroupIdCommandHandler : IRequestHandler<CreateAdUnitCacheByGroupIdCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
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
            var adUnits = await GetAdUnitList(request.AdGroupId, cancellationToken);
            if (adUnits.HasValue())
            {
                foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
                {
                    if (platform != Platform.Unknown)
                    {
                        var obj = PrepareAnalytics(adUnits, platform);
                        var redisKey = CacheKey.Units(request.AdGroupId, platform);
                        var redisValue = JsonHelper.Serialize(obj);
                        await AdsRedis.StringSetAsync(redisKey, redisValue);
                    }
                }
            }

            return Unit.Value;
        }

        private IEnumerable<AdUnitCacheDto> PrepareAnalytics(IEnumerable<AdUnitCacheDto> adUnits, Platform platform)
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

            return adUnits;
        }

        private async Task<IEnumerable<AdUnitCacheDto>> GetAdUnitList(long adGroupId, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            var result = new List<AdUnitCacheDto>();

            do
            {
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, adGroupId), cancellationToken);
                if (getAdUnitList.Ok)
                {
                    foreach (var adUnit in getAdUnitList.Data)
                    {
                        await Mediator.Send(new InitialAdCacheCommand(adUnit.Id), cancellationToken);
                        var item = Mapper.Map<AdUnitCacheDto>(adUnit);
                        result.Add(item);
                    }
                }
                next = getAdUnitList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            return result;
        }
    }
}
