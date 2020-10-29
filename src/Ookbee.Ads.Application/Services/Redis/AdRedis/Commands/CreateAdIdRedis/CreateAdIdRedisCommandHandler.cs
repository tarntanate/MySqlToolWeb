using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById;
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
            var getAdPlatformList = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdPlatformList.IsSuccess)
            {
                var ad = getAdPlatformList.Data;

                var redisKey = CacheKey.AdIds();
                var redisValue = (RedisValue)ad.Id;
                await AdsRedis.SetAddAsync(redisKey, redisValue, CommandFlags.FireAndForget);

                if (ad.Status == AdStatusType.Publish)
                {
                    redisKey = CacheKey.UnitAdIds(ad.AdUnit.Id);
                    await AdsRedis.SetAddAsync(redisKey, redisValue, CommandFlags.FireAndForget);
                }

                if (ad.Status == AdStatusType.Preview)
                {
                    redisKey = CacheKey.UnitAdIdsPreview(ad.AdUnit.Id);
                    await AdsRedis.SetAddAsync(redisKey, redisValue, CommandFlags.FireAndForget);
                }

                var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                foreach (var platform in platforms)
                {
                    if (ad.Status == AdStatusType.Publish && ad.Platforms.Contains(platform))
                    {
                        redisKey = CacheKey.UnitAdIds(ad.AdUnit.Id, platform);
                        await AdsRedis.SetAddAsync(redisKey, redisValue, CommandFlags.FireAndForget);
                    }

                    if (ad.Status == AdStatusType.Preview && ad.Platforms.Contains(platform))
                    {
                        redisKey = CacheKey.UnitAdIdsPreview(ad.AdUnit.Id, platform);
                        await AdsRedis.SetAddAsync(redisKey, redisValue, CommandFlags.FireAndForget);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
