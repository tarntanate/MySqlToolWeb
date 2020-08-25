using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.UpdateAdAssetCache
{
    public class UpdateAdAssetCacheCommandHandler : IRequestHandler<UpdateAdAssetCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public UpdateAdAssetCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateAdAssetCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdById.Ok)
            {
                var ad = getAdById.Data;
                var redisKey = CacheKey.Ad(ad.Id);
                var redisValue = (RedisValue)ad.Id;
                await AdsRedis.StringSetAsync(redisKey, redisValue);
                foreach (Platform platform in Enum.GetValues(typeof(Platform)))
                {
                    redisKey = CacheKey.UnitsAdIds(ad.AdUnit.Id, platform);
                    redisValue = (RedisValue)request.AdId;
                    var isExits = ad.Platforms.Any(x => x == platform);
                    if (isExits && (ad.Status == AdStatus.Publish || ad.Status == AdStatus.Preview))
                        await AdsRedis.SetAddAsync(redisKey, redisValue);
                    else
                        await AdsRedis.SetRemoveAsync(redisKey, redisValue);
                }
            }

            return Unit.Value;
        }

        private AdCacheDto PrepareAdNetworkUnit(AdDto ad)
        {
            var analyticsBaseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
            var result = new AdCacheDto()
            {
                CountdownSecond = ad.CountdownSecond,
                ForegroundColor = ad.ForegroundColor,
                BackgroundColor = ad.BackgroundColor,
                LinkUrl = ad.LinkUrl,
                UnitType = ad.AdUnit.AdGroup.AdUnitType.Name,
                Assets = ad.Assets.Select(asset => new AdAssetCacheDto()
                {
                    Position = asset.Position,
                    AssetType = asset.AssetType,
                    AssetPath = asset.AssetPath,
                }),
                Analytics = new AdAnalyticsCacheDto()
                {
                    Clicks = new List<string>() { $"{analyticsBaseUrl}/api/units/{ad.AdUnit.Id}/ads/{ad.Id}/stats?event=click" },
                    Impressions = new List<string>() { $"{analyticsBaseUrl}/api/units/{ad.AdUnit.Id}/ads/{ad.Id}/stats?event=impression" },
                }
            };

            if (ad.Analytics.HasValue())
            {
                result.Analytics.Impressions.Union(ad.Analytics).ToList();
            }

            return result;
        }
    }
}
