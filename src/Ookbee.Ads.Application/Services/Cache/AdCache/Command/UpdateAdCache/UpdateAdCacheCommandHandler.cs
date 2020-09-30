using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.DeleteAdCache;
using Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.DeleteAdStatsCache;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.UpdateAdCache
{
    public class UpdateAdCacheCommandHandler : IRequestHandler<UpdateAdCacheCommand>
    {
        private readonly IMapper Mapper;
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public UpdateAdCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateAdCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdById.IsSuccess &&
                getAdById.Data.HasValue())
            {
                var ad = getAdById.Data;
                if (ad.Status == AdStatusType.Publish ||
                    ad.Status == AdStatusType.Preview)
                {
                    var adCache = Mapper.Map<AdCacheDto>(ad);
                    foreach (var platform in EnumHelper.GetValues<AdPlatform>())
                    {
                        if (platform != AdPlatform.Unknown)
                        {
                            var redisKey = CacheKey.AdPlatforms(ad.Id);
                            var hashField = platform.ToString();
                            var hashValue = (RedisValue)JsonHelper.Serialize(adCache);
                            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);

                            redisKey = CacheKey.UnitAdIds(ad.AdUnit.Id, platform);
                            hashValue = (RedisValue)ad.Id;
                            if (getAdById.Data.Platforms.Any(x => x == platform))
                                await AdsRedis.SetAddAsync(redisKey, hashValue);
                            else
                                await AdsRedis.SetRemoveAsync(redisKey, hashValue);
                        }
                    }
                }
                else
                {
                    await Mediator.Send(new DeleteAdCacheCommand(request.AdId), cancellationToken);
                    await Mediator.Send(new DeleteAdStatsCacheCommand(request.AdId), cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
