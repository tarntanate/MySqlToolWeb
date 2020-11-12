using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupIdList;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupStatsRedis;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitRedis;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupRedis
{
    public class CreateAdGroupRedisCommandHandler : IRequestHandler<CreateAdGroupRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdGroupRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                var getAdGroupIdList = await Mediator.Send(new GetAdGroupIdListQuery(start, length, null, null, true), cancellationToken);
                if (getAdGroupIdList.IsSuccess)
                {
                    var adGroupIds = getAdGroupIdList.Data;
                    var redisKey = CacheKey.GroupIds();
                    foreach (var adGroupId in adGroupIds)
                    {
                        await AdsRedis.SetAddAsync(redisKey, adGroupId, CommandFlags.FireAndForget);
                        await Mediator.Send(new CreateAdGroupStatsRedisCommand(request.CaculatedAt, adGroupId));
                        await Mediator.Send(new CreateAdUnitRedisCommand(request.CaculatedAt, adGroupId));
                    }
                    next = adGroupIds.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
