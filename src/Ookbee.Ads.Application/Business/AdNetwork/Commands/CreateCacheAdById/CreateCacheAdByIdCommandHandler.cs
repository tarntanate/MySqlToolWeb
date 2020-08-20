﻿using MediatR;
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

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheAdById
{
    public class CreateCacheAdByIdCommandHandler : IRequestHandler<CreateCacheAdByIdCommand, Unit>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateCacheAdByIdCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(CreateCacheAdByIdCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId));
            if (getAdById.Ok)
            {
                var data = PrepareAdNetworkUnit(getAdById.Data);
                var redisKey = CacheKey.Ad(request.AdId);
                var redisValue = (RedisValue)JsonHelper.Serialize(data);
                if (await AdsRedis.KeyExistsAsync(redisKey))
                    await AdsRedis.KeyDeleteAsync(redisKey);
                await AdsRedis.StringSetAsync(redisKey, redisValue);
            }

            return Unit.Value;
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
