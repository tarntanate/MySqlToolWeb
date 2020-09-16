using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.DeleteAdCache;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.DeleteAdStatsCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCache
{
    public class UpdateAdCacheCommandHandler : IRequestHandler<UpdateAdCacheCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

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
            if (getAdById.Ok)
            {
                var ad = getAdById.Data;
                if (ad.Status == AdStatus.Publish ||
                    ad.Status == AdStatus.Preview)
                {
                    var adCache = Mapper.Map<AdCacheDto>(ad);
                    foreach (var platform in EnumHelper.GetValues<Platform>())
                    {
                        if (platform != Platform.Unknown)
                        {
                            var redisKey = CacheKey.Ad(ad.Id);
                            var hashField = platform.ToString();
                            var hashValue = (RedisValue)JsonHelper.Serialize(adCache);
                            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);

                            redisKey = CacheKey.UnitsAdIds(ad.AdUnit.Id, platform);
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
