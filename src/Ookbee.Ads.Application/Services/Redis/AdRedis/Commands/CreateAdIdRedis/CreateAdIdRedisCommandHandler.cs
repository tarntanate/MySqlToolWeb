using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdPlatformList;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdIdRedis
{
    public class CreateAdIdRedisCommandHandler : IRequestHandler<CreateAdIdRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdIdRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdIdRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                var getAdPlatformList = await Mediator.Send(new GetAdPlatformListQuery(start, length, request.AdUnitId, null), cancellationToken);
                if (getAdPlatformList.IsSuccess)
                {
                    var ads = getAdPlatformList.Data;
                    
                    var redisKey = CacheKey.AdIds();
                    var redisValues = ads.Select(ad => (RedisValue)ad.Id).ToArray();
                    await AdsRedis.SetAddAsync(redisKey, redisValues, CommandFlags.FireAndForget);

                    redisKey = CacheKey.UnitAdIds(request.AdUnitId);
                    redisValues = ads.Where(ad => ad.Status == AdStatusType.Publish).Select(item => (RedisValue)item.Id).ToArray();
                    await AdsRedis.SetAddAsync(redisKey, redisValues, CommandFlags.FireAndForget);

                    redisKey = CacheKey.UnitAdIdsPreview(request.AdUnitId);
                    redisValues = ads.Where(ad => ad.Status == AdStatusType.Preview).Select(item => (RedisValue)item.Id).ToArray();
                    await AdsRedis.SetAddAsync(redisKey, redisValues, CommandFlags.FireAndForget);

                    var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                    foreach (var platform in platforms)
                    {
                        redisValues = ads.Where(ad => ad.Status == AdStatusType.Publish && ad.Platforms.Contains(platform)).Select(item => (RedisValue)item.Id).ToArray();
                        if (redisValues.HasValue())
                        {
                            redisKey = CacheKey.UnitAdIds(request.AdUnitId, platform);
                            await AdsRedis.SetAddAsync(redisKey, redisValues, CommandFlags.FireAndForget);
                        }

                        redisValues = ads.Where(ad => ad.Status == AdStatusType.Preview && ad.Platforms.Contains(platform)).Select(item => (RedisValue)item.Id).ToArray();
                        if (redisValues.HasValue())
                        {
                            redisKey = CacheKey.UnitAdIdsPreview(request.AdUnitId, platform);
                            await AdsRedis.SetAddAsync(redisKey, redisValues, CommandFlags.FireAndForget);
                        }
                    }

                    next = ads.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
