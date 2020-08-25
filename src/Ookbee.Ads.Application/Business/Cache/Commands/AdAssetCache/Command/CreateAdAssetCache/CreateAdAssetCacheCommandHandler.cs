using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.CreateAdAssetCache
{
    public class CreateAdAssetCacheCommandHandler : IRequestHandler<CreateAdAssetCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdAssetCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(CreateAdAssetCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdById.Ok)
            {
                var ad = getAdById.Data;
                var adAsset = PrepareAdAssetCache(ad);
                var redisKey = CacheKey.Ad(ad.Id);
                var redisValue = (RedisValue)JsonHelper.Serialize(adAsset);
                var platforms = getAdById.Data.Platforms.Select(x => x.ToString());
                await AdsRedis.StringSetAsync(redisKey, redisValue);
                foreach (var platform in platforms)
                {
                    redisKey = CacheKey.AdIdsByUnit(ad.AdUnit.Id, platform);
                    redisValue = (RedisValue)request.AdId;
                    await AdsRedis.SetAddAsync(redisKey, redisValue);
                }
            }

            return Unit.Value;
        }

        private AdCacheDto PrepareAdAssetCache(AdDto ad)
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
                    Clicks = new List<string>() { $"{analyticsBaseUrl}/api/stats/units/{ad.AdUnit.Id}/ads/{ad.Id}?event=click" },
                    Impressions = new List<string>() { $"{analyticsBaseUrl}/api/stats/units/{ad.AdUnit.Id}/ads/{ad.Id}?event=impression" },
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
