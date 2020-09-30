using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitIdList;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdIdRedis;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitStatsRedis;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
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
            var start = 0;
            var length = 10;
            var next = false;
            do
            {
                var getAdUnitIdList = await Mediator.Send(new GetAdUnitIdListQuery(start, length, request.AdGroupId), cancellationToken);
                if (getAdUnitIdList.IsSuccess)
                {
                    var redisKey = string.Empty;
                    var redisValue = string.Empty;
                    var items = getAdUnitIdList.Data;
                    foreach (var adUnitId in items)
                    {
                        redisKey = CacheKey.UnitIds();
                        await AdsRedis.SetAddAsync(redisKey, adUnitId, CommandFlags.FireAndForget);

                        redisKey = CacheKey.GroupUnitIds(request.AdGroupId);
                        await AdsRedis.SetAddAsync(redisKey, adUnitId, CommandFlags.FireAndForget);

                        await Mediator.Send(new CreateAdUnitStatsRedisCommand(request.CaculatedAt, adUnitId));
                        await Mediator.Send(new CreateAdIdRedisCommand(adUnitId));
                    }
                    next = items.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
