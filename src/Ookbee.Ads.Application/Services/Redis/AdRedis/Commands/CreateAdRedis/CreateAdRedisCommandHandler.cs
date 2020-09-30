using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdIdRedis;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdStatsRedis;
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

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdRedis
{
    public class CreateAdRedisCommandHandler : IRequestHandler<CreateAdRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var getAdList = await Mediator.Send(new GetAdListQuery(start, length, request.AdUnitId, null), cancellationToken);
                if (getAdList.IsSuccess)
                {
                    var ads = getAdList.Data.Where(x => x.Status != AdStatusType.Publish || x.Status != AdStatusType.Preview);
                    foreach (var ad in ads)
                    {
                        var baseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                        var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                        foreach (var platform in platforms)
                        {
                            var redisValues = new
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
                                redisValues.Analytics.HasValue())
                                redisValues.Analytics.Impressions.AddRange(ad.Analytics);

                            var redisKey = CacheKey.AdPlatforms(ad.Id);
                            var hashField = platform.ToString();
                            var hashValue = (RedisValue)JsonHelper.Serialize(redisValues);
                            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue, When.Always, CommandFlags.FireAndForget);
                        }
                        await Mediator.Send(new CreateAdStatsRedisCommand(request.CaculatedAt, ad.Id));
                        await Mediator.Send(new CreateAdIdRedisCommand(ad.Id));
                    }
                    next = ads.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }

        private void CreateAdIds()
        {
            
        }
    }
}
