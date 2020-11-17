using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitIdRedis
{
    public class CreateAdUnitIdRedisCommandHandler : IRequestHandler<CreateAdUnitIdRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdUnitIdRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUnitIdRedisCommand request, CancellationToken cancellationToken)
        {
            var redisKey = string.Empty;
            var redisValue = string.Empty;

            redisKey = RedisKeys.GroupUnitIds(request.AdGroupId);
            redisValue = (RedisValue)request.AdUnitId;
            await AdsRedis.SetAddAsync(redisKey, redisValue, CommandFlags.FireAndForget);

            redisKey = RedisKeys.UnitIds();
            redisValue = (RedisValue)request.AdUnitId;
            await AdsRedis.SetAddAsync(redisKey, redisValue, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
