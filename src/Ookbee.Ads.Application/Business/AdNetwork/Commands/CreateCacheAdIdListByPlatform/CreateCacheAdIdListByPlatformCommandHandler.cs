using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheAdIdListByPlatform
{
    public class CreateCacheAdIdListByPlatformCommandHandler : IRequestHandler<CreateCacheAdIdListByPlatformCommand, Unit>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateCacheAdIdListByPlatformCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(CreateCacheAdIdListByPlatformCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in request.Platforms)
            {
                var redisKey = CacheKey.AdIdsByUnit(request.AdUnitId, platform.ToString());
                var redisValue = (RedisValue)request.AdId;
                if (await AdsRedis.KeyExistsAsync(redisKey))
                    await AdsRedis.KeyDeleteAsync(redisKey);
                await AdsRedis.SetAddAsync(redisKey, redisValue);
            }

            return Unit.Value;
        }
    }
}
