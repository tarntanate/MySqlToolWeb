using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
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

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdUnitById
{
    public class CreateAdUnitByIdCommandHandler : IRequestHandler<CreateAdUnitByIdCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }
        private AdsRedisContext AdsRedis { get; }

        public CreateAdUnitByIdCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
            AdsRedis = adsRedis;
        }

        public async Task<HttpResult<bool>> Handle(CreateAdUnitByIdCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId));
            if (getAdById.Ok)
            {
                var ad = getAdById.Data;
                var data = PrepareAdNetworkUnit(ad);
                string redisKey;
                string redisValue;
                foreach (var platform in ad.Platforms)
                {
                    redisKey = CacheKey.AdIdByUnit(ad.AdUnit.Id);
                    redisValue = (RedisValue)ad.Id;
                    if (await AdsRedis.Database(1).KeyExistsAsync(redisKey))
                        await AdsRedis.Database(1).KeyDeleteAsync(redisKey);
                    await AdsRedis.Database(1).SetAddAsync(redisKey, redisValue);

                    redisKey = CacheKey.AdIdByUnit(ad.AdUnit.Id, platform.ToString());
                    redisValue = (RedisValue)ad.Id;
                    if (await AdsRedis.Database(1).KeyExistsAsync(redisKey))
                        await AdsRedis.Database(1).KeyDeleteAsync(redisKey);
                    await AdsRedis.Database(1).SetAddAsync(redisKey, redisValue);
                }

                redisKey = CacheKey.AssetByAd(ad.Id);
                redisValue = (RedisValue)JsonHelper.Serialize(data);
                if (await AdsRedis.Database(2).KeyExistsAsync(redisKey))
                    await AdsRedis.Database(2).KeyDeleteAsync(redisKey);
                await AdsRedis.Database(2).StringSetAsync(redisKey, redisValue);
            }

            return result.Success(true);
        }

        private AdNetworkUnitDto PrepareAdNetworkUnit(AdDto ad)
        {
            var analyticsBaseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
            var result = new AdNetworkUnitDto()
            {
                CountdownSecond = ad.CountdownSecond,
                ForegroundColor = ad.ForegroundColor,
                BackgroundColor = ad.BackgroundColor,
                LinkUrl = ad.LinkUrl,
                UnitType = ad.AdUnit.AdGroup.AdUnitType.Name,
                Assets = ad.Assets.Select(asset => new AdNetworkUnitAssetDto()
                {
                    Position = asset.Position,
                    AssetType = asset.AssetType,
                    AssetPath = asset.AssetPath,
                }),
                Analytics = new AdNetworkUnitAnalyticsDto()
                {
                    Clicks = new List<string>() { $"{analyticsBaseUrl}/api/ads/{ad.Id}/stats?event=click" },
                    Impressions = new List<string>() { $"{analyticsBaseUrl}/api/ads/{ad.Id}/stats?event=impression" },
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
