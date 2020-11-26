using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdByPlatformRedis
{
    public class CreateAdByPlatformRedisCommandHandler : IRequestHandler<CreateAdByPlatformRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdByPlatformRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdByPlatformRedisCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdById.IsSuccess)
            {
                var ad = getAdById.Data;
                var baseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                foreach (var platform in platforms)
                {
                    var redisKey = RedisKeys.AdPlatforms(ad.Id);
                    var hashField = platform.ToString();
                    if (ad.Platforms.Contains(platform))
                    {
                        var adCache = new AdCacheDto()
                        {
                            CountdownSecond = ad.CountdownSecond,
                            ForegroundColor = ad.ForegroundColor,
                            BackgroundColor = ad.BackgroundColor,
                            LinkUrl = ad.LinkUrl,
                            UnitType = ad.AdUnit.AdGroup.AdGroupType.Name,
                            Assets = ad.Assets?.Where(asset => asset.DeletedAt == null)
                                .Select(asset => new AdAssetCacheDto
                            {
                                Position = asset.Position,
                                Type = asset.AssetType,
                                Url = asset.AssetUrl,
                            }),
                            Analytics = new AnalyticsCacheDto
                            {
                                Clicks = new List<string>() { $"{baseUrl}/api/ads/{ad.Id}/stats?type={AdStatsType.Click}&platform={platform}&campaignId={ad.Campaign.Id}&unitId={ad.AdUnit.Id}&publisherId={ad.AdUnit.AdGroup.Publisher.Id}".ToLower() },
                                Impressions = new List<string>() { $"{baseUrl}/api/ads/{ad.Id}/stats?type={AdStatsType.Impression}&platform={platform}&campaignId={ad.Campaign.Id}&unitId={ad.AdUnit.Id}&publisherId={ad.AdUnit.AdGroup.Publisher.Id}".ToLower() }
                            }
                        };

                        if (ad.Analytics.HasValue() &&
                            adCache.Analytics.HasValue())
                            adCache.Analytics.Impressions.Union(ad.Analytics);

                        var cacheObj = new ApiItemResult<AdCacheDto>();
                        cacheObj.Data = adCache;

                        var hashValue = (RedisValue)JsonHelper.Serialize(cacheObj);
                        await AdsRedis.HashSetAsync(redisKey, hashField, hashValue, When.Always, CommandFlags.FireAndForget);
                    }
                    else
                    {
                        await AdsRedis.HashDeleteAsync(redisKey, hashField, CommandFlags.FireAndForget);
                    }
                }
            }

            return Unit.Value;
        }
    }
}


