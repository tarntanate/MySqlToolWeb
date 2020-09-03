using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.DeleteAdAssetCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.UpdateAdAssetCache
{
    public class UpdateAdAssetCacheCommandHandler : IRequestHandler<UpdateAdAssetCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public UpdateAdAssetCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateAdAssetCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdById.Ok)
            {
                var ad = getAdById.Data;
                if (ad.Status == AdStatus.Publish ||
                    ad.Status == AdStatus.Preview)
                {
                    var adCache = Mapper.Map<AdCacheDto>(ad);
                    foreach (Platform platform in Enum.GetValues(typeof(Platform)))
                    {
                        var redisKey = CacheKey.Ad(ad.Id, platform);
                        var redisValue = (RedisValue)JsonHelper.Serialize(adCache);
                        await AdsRedis.StringSetAsync(redisKey, redisValue);

                        redisKey = CacheKey.UnitsAdIds(ad.AdUnit.Id, platform);
                        redisValue = (RedisValue)ad.Id;
                        if (getAdById.Data.Platforms.Any(x => x == platform))
                            await AdsRedis.SetAddAsync(redisKey, redisValue);
                        else
                            await AdsRedis.SetRemoveAsync(redisKey, redisValue);
                    }
                }
                else
                {
                    await Mediator.Send(new DeleteAdAssetCacheCommand(request.AdId), cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
