using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdIdListByPlatform
{
    public class CreateAdIdListByPlatformCommandHandler : IRequestHandler<CreateAdIdListByPlatformCommand, Unit>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdIdListByPlatformCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(CreateAdIdListByPlatformCommand request, CancellationToken cancellationToken)
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
