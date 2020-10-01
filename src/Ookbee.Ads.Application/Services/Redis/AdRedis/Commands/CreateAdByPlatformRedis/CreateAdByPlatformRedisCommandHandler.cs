using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
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
            var getAdList = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdList.IsSuccess)
            {
                var ad = getAdList.Data;
                var baseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                foreach (var platform in platforms)
                {
                    var adCache = new
                    {
                        CountdownSecond = ad.CountdownSecond,
                        ForegroundColor = ad.ForegroundColor,
                        BackgroundColor = ad.BackgroundColor,
                        LinkUrl = ad.LinkUrl,
                        UnitType = ad.AdUnit.AdGroup.AdUnitType.Name,
                        Assets = ad.Assets.Select(asset => new
                        {
                            Position = asset.Position,
                            AssetType = asset.AssetType,
                            AssetPath = asset.AssetPath,
                        }),
                        Analytics = new
                        {
                            Clicks = new List<string>() { $"{baseUrl}/api/ads/{ad.Id}/stats?platform={platform}&type={AdStatsType.Click}&campaignId={ad.Campaign.Id}".ToLower() },
                            Impressions = new List<string>() { $"{baseUrl}/api/ads/{ad.Id}/stats?platform={platform}&type={AdStatsType.Impression}&campaignId={ad.Campaign.Id}".ToLower() }
                        }
                    };

                    if (ad.Analytics.HasValue() &&
                        adCache.Analytics.HasValue())
                        adCache.Analytics.Impressions.AddRange(ad.Analytics);

                    var redisKey = CacheKey.AdPlatforms(ad.Id);
                    var hashField = platform.ToString();
                    var hashValue = (RedisValue)JsonHelper.Serialize(adCache);
                    await AdsRedis.HashSetAsync(redisKey, hashField, hashValue, When.Always, CommandFlags.FireAndForget);
                }
            }

            return Unit.Value;
        }
    }
}
