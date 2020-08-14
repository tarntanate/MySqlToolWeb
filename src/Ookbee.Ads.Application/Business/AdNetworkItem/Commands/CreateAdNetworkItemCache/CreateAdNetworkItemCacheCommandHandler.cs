using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetworkItem.Commands.CreateAdNetworkItemCache
{
    public class CreateAdNetworkItemCacheCommandHandler : IRequestHandler<CreateAdNetworkItemCacheCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdNetworkItemCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<bool>> Handle(CreateAdNetworkItemCacheCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var start = 0;
            var length = 100;
            var next = true;

            do
            {
                var getAdList = await Mediator.Send(new GetAdListQuery(start, length, null, null));
                if (getAdList.Ok)
                {
                    var ads = getAdList.Data;
                    foreach (var ad in ads)
                    {
                        string redisKey;
                        string redisValue;
                        var data = PrepareAdNetworkItem(ad);

                        foreach (var platform in ad.Platforms)
                        {
                            redisKey = CacheKey.AdIdByUnit(ad.AdUnit.AdNetworkUnitId);
                            redisValue = (RedisValue)ad.Id;
                            await AdsRedis.SetAddAsync(redisKey, redisValue);

                            redisKey = CacheKey.AdIdByUnit(ad.AdUnit.AdNetworkUnitId, platform.ToString());
                            redisValue = (RedisValue)ad.Id;
                            await AdsRedis.SetAddAsync(redisKey, redisValue);
                        }

                        redisKey = CacheKey.AssetByAd(ad.Id);
                        redisValue = (RedisValue)JsonHelper.Serialize(data);
                        await AdsRedis.StringSetAsync(redisKey, redisValue);
                    }
                }
                next = getAdList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            return result.Success(true);
        }

        private AdNetworkItemDto PrepareAdNetworkItem(AdDto ad)
        {
            var analyticsBaseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
            var result = new AdNetworkItemDto()
            {
                Data = new AdNetworkItemDataDto()
                {
                    CountdownSecond = ad.CountdownSecond,
                    ForegroundColor = ad.ForegroundColor,
                    BackgroundColor = ad.BackgroundColor,
                    LinkUrl = ad.LinkUrl,
                    UnitType = ad.AdUnit.AdGroup.AdUnitType.Name,
                    Assets = ad.Assets.Select(asset => new AdNetworkItemAssetDto()
                    {
                        Position = asset.Position,
                        AssetType = asset.AssetType,
                        AssetPath = asset.AssetPath,
                    }),
                    Analytics = new AdNetworkItemAnalyticsDto()
                    {
                        Clicks = new List<string>() { $"{analyticsBaseUrl}/api/ads/{ad.Id}/stats?event=click" },
                        Impressions = new List<string>() { $"{analyticsBaseUrl}/api/ads/{ad.Id}/stats?event=impression" },
                    }
                }
            };

            if (ad.Analytics.HasValue())
            {
                result.Data.Analytics.Impressions.Union(ad.Analytics).ToList();
            }

            return result;
        }
    }
}
